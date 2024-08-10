using DongleSimulator.Domain.Custom;
using DongleSimulator.Domain.DTOs;
using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DongleSimulator.Infra.Repositories;

public class DashboardRepository : IDashboardReadOnlyRepository
{
    private readonly DongleSimulatorDbContext _dbContext;

    public DashboardRepository(DongleSimulatorDbContext dbContext) => _dbContext = dbContext;

    public async Task<PagedList<Source>> FilterSources(FilterSourceDto filterSourceDto, int page, int pageSize)
    {
        var sourcesQuery = _dbContext.Sources
            .AsNoTracking()
            .Include(s => s.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        if (filterSourceDto.Status.Any())
        {
            sourcesQuery = sourcesQuery.Where(s => filterSourceDto.Status.Contains(s.Status));
        }

        if (!filterSourceDto.Title.IsNullOrEmpty())
        {
            sourcesQuery = 
                sourcesQuery.Where(s => s.Title.Contains(filterSourceDto.Title!) ||
                                        s.Subtitle.Contains(filterSourceDto.Title!));
        }
        
        if (!filterSourceDto.Username.IsNullOrEmpty())
        {
            sourcesQuery = sourcesQuery.Where(s => s.User.Name.Equals(filterSourceDto.Username));
        }

        if (filterSourceDto.OrderByMostRecent is null || filterSourceDto.OrderByMostRecent == true)
        {
            sourcesQuery = sourcesQuery.OrderByDescending(s => s.CreatedAt);
        }

        var count = await _dbContext.Sources.CountAsync();
        var sources = await sourcesQuery.ToListAsync();
        
        return new PagedList<Source>
        {
            Items = sources,
            Page = page,
            PageSize = pageSize,
            TotalCount = count
        };
    }
    
    public async Task<PagedList<Template>> FilterTemplates(FilterTemplateDto filterTemplateDto, int page, int pageSize)
    {
        var templatesQuery = _dbContext.Templates
            .AsNoTracking()
            .Include(s => s.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        if (filterTemplateDto.Status.Any())
        {
            templatesQuery = templatesQuery.Where(s => filterTemplateDto.Status.Contains(s.Status));
        }

        if (!filterTemplateDto.Title.IsNullOrEmpty())
        {
            templatesQuery = 
                templatesQuery.Where(s => s.Title.Contains(filterTemplateDto.Title!) ||
                                        s.Subtitle.Contains(filterTemplateDto.Title!));
        }
        
        if (!filterTemplateDto.Username.IsNullOrEmpty())
        {
            templatesQuery = templatesQuery.Where(s => s.User.Name.Equals(filterTemplateDto.Username));
        }

        if (filterTemplateDto.OrderByMostRecent is null || filterTemplateDto.OrderByMostRecent == true)
        {
            templatesQuery = templatesQuery.OrderByDescending(s => s.CreatedAt);
        }

        if (filterTemplateDto.Replaces is not null)
        {
            templatesQuery = templatesQuery.Where(t => t.Replaces.Equals(filterTemplateDto.Replaces));
        }
        
        var count = await _dbContext.Templates.CountAsync();
        var templates = await templatesQuery.ToListAsync();
        
        return new PagedList<Template>
        {
            Items = templates,
            Page = page,
            PageSize = pageSize,
            TotalCount = count
        };
    }
    
    public async Task<Source?> GetSourceById(long id)
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id.Equals(id));
    }
    
    public async Task<Template?> GetTemplateById(long id)
    {
        return await _dbContext.Templates
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id.Equals(id));
    }
    
    public async Task<PagedList<User>> GetAllUsers(int page, int pageSize)
    {
        var totalCount = await _dbContext.Sources.CountAsync();

        var users = await _dbContext.Users
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<User>
        {
            Items = users,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}