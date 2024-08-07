namespace DongleSimulator.Domain.Repositories.Template;

public interface ITemplateWriteOnlyRepository
{
    public Task Create(Entities.Template template);
    public Task Delete(long id);
}