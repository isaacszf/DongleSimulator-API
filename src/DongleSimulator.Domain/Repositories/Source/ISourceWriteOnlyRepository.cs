namespace DongleSimulator.Domain.Repositories.Source;

public interface ISourceWriteOnlyRepository
{
    public Task Create(Entities.Source source);
    public Task Delete(long id);
}