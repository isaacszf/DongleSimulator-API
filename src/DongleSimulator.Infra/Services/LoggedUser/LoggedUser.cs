using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Services.LoggedUser;
using DongleSimulator.Domain.Services.Tokens;
using DongleSimulator.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DongleSimulator.Infra.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly DongleSimulatorDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUser(
        DongleSimulatorDbContext dbContext,
        ITokenProvider tokenProvider
        )
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    
    public async Task<User> User()
    {
        var token = _tokenProvider.GetTokenOnRequest();
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
        var strUserId = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext.Users
            .AsNoTracking()
            .FirstAsync(u => u.UserIdentifier.Equals(Guid.Parse(strUserId)));
    }
}