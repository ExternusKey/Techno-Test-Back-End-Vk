using AuthService.Data;
using AuthService.Models;
using AuthService.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace AuthService.Routs
{
    public class RegisterHandler
    {
        public static async Task Handle(HttpContext context, ApplicationContext dbContext)
        {
            var email = context.Request.Form["email"].FirstOrDefault();
            var password = context.Request.Form["password"].FirstOrDefault();
            
            if (!PassValidator.IsValidEmail(email))
            {
                var errorMessage = new
                {
                    Message = "Invalid email format"
                };
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
                return;
            }
          
            if (PassValidator.IsWeakPassword(password))
            {
                var errorMessage = new
                {
                    Message = "Weak password"
                };
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
                return;
            }
            
            if (await dbContext.Users.AnyAsync(u => u.Email == email))
            {
                var errorMessage = new
                {
                    Message = "Email already registered"
                };
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
                return;
            }
     
            var newUser = new User { Email = email, Password = password };

            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync();

            context.Response.StatusCode = StatusCodes.Status200OK;
            string passwordCheckStatus = PassValidator.IsPerfectPassword(password) ? "perfect" : "good";
            var response = new
            {
                newUser.UserId,
                PasswordCheckStatus = passwordCheckStatus
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
