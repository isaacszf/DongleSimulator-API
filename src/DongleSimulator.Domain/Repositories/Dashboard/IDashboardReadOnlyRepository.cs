using DongleSimulator.Domain.Custom;

namespace DongleSimulator.Domain.Repositories.Dashboard;

public interface IDashboardReadOnlyRepository
{
    public Task<PagedList<Entities.Source>> GetAllSources(int page, int pageSize);
    public Task<PagedList<Entities.Source>> GetAllSourcesByUsername(string username, int page, int pageSize);
    public Task<Entities.Source?> GetSourceById(long id);
    
    public Task<PagedList<Entities.Template>> GetAllTemplates(int page, int pageSize);
}