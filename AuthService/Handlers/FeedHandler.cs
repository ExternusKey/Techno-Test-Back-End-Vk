using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace AuthService.Routs
{
    public class FeedHandler
    {
        public static async Task Handle(HttpContext context)
        { 
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
         
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("CatwIthmeowanDsUshieaTiNggoodyeS");

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);
            }
            catch
            {
                var errorMessage = new
                {
                    Message = "Invalid token"
                };
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
                return;
            }
  
            context.Response.StatusCode = StatusCodes.Status200OK;
        }
    }
}
