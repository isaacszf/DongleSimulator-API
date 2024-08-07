namespace DongleSimulator.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    public Task Create(Entities.User user);
}