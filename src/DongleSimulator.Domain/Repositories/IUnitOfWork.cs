namespace DongleSimulator.Domain.Repositories;

public interface IUnitOfWork
{
    public Task Commit();
}