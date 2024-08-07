namespace DongleSimulator.Domain.Repositories.Template;

public interface ITemplateUpdateOnlyRepository
{
    public Task<Entities.Template?> GetByIdAdminUpdateOnly(long id);
    public void Update(Entities.Template template);
}