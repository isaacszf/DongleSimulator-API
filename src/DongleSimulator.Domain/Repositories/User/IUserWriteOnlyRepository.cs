namespace DongleSimulator.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task Create(Entities.User user);
    public Task Delete(long id);
    
    public Task<Entities.User?> GetUserById(long id);
}