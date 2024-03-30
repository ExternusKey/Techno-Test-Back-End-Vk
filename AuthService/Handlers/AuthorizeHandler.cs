using AuthService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace AuthService.Routs
{
    public class AuthorizeHandler
    {
        public static async Task Handle(HttpContext context, ApplicationContext dbContext)
        {
            var email = context.Request.Form["email"].FirstOrDefault();
            var password = context.Request.Form["password"].FirstOrDefault();
 
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                var errorMessage = new
                {
                    Message = "Invalid email or password"
                };
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
                return;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("CatwIthmeowanDsUshieaTiNggoodyeS");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            var response = new
            {
                AccessToken = tokenHandler.WriteToken(accessToken)
            };

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
