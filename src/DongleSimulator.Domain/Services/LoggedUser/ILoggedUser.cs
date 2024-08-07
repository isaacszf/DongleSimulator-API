using DongleSimulator.Domain.Entities;

namespace DongleSimulator.Domain.Services.LoggedUser;

public interface ILoggedUser
{
    public Task<User> User();
}