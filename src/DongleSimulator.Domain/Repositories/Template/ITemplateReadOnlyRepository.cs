namespace DongleSimulator.Domain.Repositories.Template;

public interface ITemplateReadOnlyRepository
{
    public Task<Entities.Template?> GetById(Entities.User user, long id);
    public Task<Entities.Template?> GetByIdAdminReadOnly(long id);
    public Task<Entities.Template?> GetRandomApproved();
}