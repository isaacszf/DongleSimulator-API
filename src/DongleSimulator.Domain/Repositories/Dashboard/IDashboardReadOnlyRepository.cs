using DongleSimulator.Domain.Custom;
using DongleSimulator.Domain.DTOs;

namespace DongleSimulator.Domain.Repositories.Dashboard;

public interface IDashboardReadOnlyRepository
{
    public Task<Entities.Source?> GetSourceById(long id);
    public Task<PagedList<Entities.Source>> FilterSources(FilterSourceDto filterSourceDto, int page, int pageSize);
    
    public Task<Entities.Template?> GetTemplateById(long id);
    public Task<PagedList<Entities.Template>> FilterTemplates(FilterTemplateDto filterTemplateDto, int page, int pageSize);
    
    public Task<PagedList<Entities.User>> GetAllUsers(int page, int pageSize);
}