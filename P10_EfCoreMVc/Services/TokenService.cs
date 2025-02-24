
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Interfaces;


public class TokenService : ITokenService   // inheritance 

{
    private readonly string _secretKey;     // 

    public TokenService(IConfiguration configuration)
    {
        _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentNullException("SecretKey is not configured.");
    }


    public string CreateToken(string userId, string email, string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();   // intializing new instance of  JwtSecurityTokenHandler

        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor      // sign contract 
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username)
            ]),
            Expires = DateTime.UtcNow.AddHours(24),

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }


    public Guid VerifyTokenAndGetId(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_secretKey);


            var validationParameters = new TokenValidationParameters   // 
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var validate = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);


            var userId = validate.FindFirst(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                return Guid.Parse(userId.Value);
            }
            else
            {
                throw new Exception("User ID not found in token.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Token validation failed: " + ex.Message);
        }
    }


}

