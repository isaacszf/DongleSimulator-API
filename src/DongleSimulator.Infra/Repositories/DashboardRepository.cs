using DongleSimulator.Domain.Custom;
using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DongleSimulator.Infra.Repositories;

public class DashboardRepository : IDashboardReadOnlyRepository
{
    private readonly DongleSimulatorDbContext _dbContext;

    public DashboardRepository(DongleSimulatorDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<PagedList<Source>> GetAllSources(int page, int pageSize)
    {
        var totalCount = await _dbContext.Sources.CountAsync();
        
        var sources = await _dbContext.Sources
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Source>
        {
            Items = sources,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    
    public async Task<PagedList<Template>> GetAllTemplates(int page, int pageSize)
    {
        var totalCount = await _dbContext.Sources.CountAsync();
        
        var templates = await _dbContext.Templates
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedList<Template>
        {
            Items = templates,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    
    public async Task<PagedList<Source>> GetAllSourcesByUsername(string username, int page, int pageSize)
    {
        var totalCount = await _dbContext.Sources.CountAsync();
        
        var sources = await _dbContext.Sources
            .AsNoTracking()
            .Include(s => s.User)
            .Where(s => s.User.Name.Equals(username))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedList<Source>
        {
            Items = sources,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<Source?> GetSourceById(long id)
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id.Equals(id));
    }
    
    public async Task<PagedList<Template>> GetAllTemplatesByUsername(string username, int page, int pageSize)
    {
        var totalCount = await _dbContext.Templates.CountAsync();
        
        var templates = await _dbContext.Templates
            .AsNoTracking()
            .Include(s => s.User)
            .Where(s => s.User.Name.Equals(username))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PagedList<Template>
        {
            Items = templates,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
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