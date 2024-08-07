using DongleSimulator.Domain.Entities;

namespace DongleSimulator.Domain.Services.Tokens;

public interface IAccessToken
{
    public string Generate(User user);
}