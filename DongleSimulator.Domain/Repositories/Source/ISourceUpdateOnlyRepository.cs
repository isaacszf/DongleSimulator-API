namespace DongleSimulator.Domain.Repositories.Source;

public interface ISourceUpdateOnlyRepository
{
    public Task<Entities.Source?> GetByIdAdminUpdateOnly(long id);
    public void Update(Entities.Source source);
}