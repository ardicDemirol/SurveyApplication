using Microsoft.IdentityModel.Tokens;
using SurveyApplication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyApplication.Extensions;
public class JWTProvider(IConfiguration configuration) : IJWTProvider
{
    private readonly IConfiguration _configuration = configuration;
    public string GenerateJWTToken(string email, string role)
    {
        var claims = new List<Claim>
        {
        new (ClaimTypes.NameIdentifier, email),
        new (ClaimTypes.Role,role)
        };

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                SecurityAlgorithms.HmacSha256Signature)
            );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

}
