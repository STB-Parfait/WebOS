using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{
    public string GenerateToken(UserResponseDTO user)
    {
        var keyString = Environment.GetEnvironmentVariable("JWT_SECRET");
        var key = Encoding.ASCII.GetBytes(keyString);

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.username),
            }),
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);

        return tokenHandler.WriteToken(token);
    }
}