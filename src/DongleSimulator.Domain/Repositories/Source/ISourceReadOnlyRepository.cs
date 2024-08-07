namespace DongleSimulator.Domain.Repositories.Source;

public interface ISourceReadOnlyRepository
{
    public Task<Entities.Source?> GetById(Entities.User user, long id);
    public Task<Entities.Source?> GetByIdAdminReadOnly(long id);
    public Task<Entities.Source?> GetRandomApproved();
}