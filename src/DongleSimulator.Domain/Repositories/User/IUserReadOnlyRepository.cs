using DongleSimulator.Domain.Custom;

namespace DongleSimulator.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    public Task<bool> ExistsUserWithUsername(string username);
    public Task<bool> ExistsUserWithEmail(string email);
    public Task<Entities.User?> GetUserByEmailAndPassword(string email, string password);
}