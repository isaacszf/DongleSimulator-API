using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Services.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace DongleSimulator.Infra.Services.Tokens;

public class JwtTokenService : IAccessToken
{
    private readonly int _expirationTimeMinutes;
    private readonly string _apiKey;

    public JwtTokenService(int expirationTimeMinutes, string apiKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _apiKey = apiKey;
    }
    
    public string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_apiKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}