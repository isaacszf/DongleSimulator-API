using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories.Template;
using DongleSimulator.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DongleSimulator.Infra.Repositories;

public class TemplateRepository : ITemplateWriteOnlyRepository, ITemplateReadOnlyRepository, ITemplateUpdateOnlyRepository
{
    private readonly DongleSimulatorDbContext _dbContext;

    public TemplateRepository(DongleSimulatorDbContext dbContext) => _dbContext = dbContext;

    public async Task Create(Template template) => await _dbContext.Templates.AddAsync(template);

    public async Task Delete(long id)
    {
        var template = await _dbContext.Templates.FindAsync(id);
        _dbContext.Templates.Remove(template!);
    }

    public async Task<Template?> GetById(User user, long id)
    {
        return await _dbContext.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.UserId.Equals(user.Id) && t.Id.Equals(id));
    }

    public async Task<Template?> GetByIdAdminReadOnly(long id)
    {
        return await _dbContext.Templates
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id.Equals(id));
    }

    public async Task<Template?> GetByIdAdminUpdateOnly(long id)
    {
        return await _dbContext.Templates
            .FirstOrDefaultAsync(s => s.Id.Equals(id));
    }

    public async Task<Template?> GetRandomApproved()
    {
        return await _dbContext.Templates
            .AsNoTracking()
            .Where(t => t.Status == Status.Approved)
            .OrderBy(_ => Guid.NewGuid())
            .FirstOrDefaultAsync();
    }
    
    public void Update(Template template) => _dbContext.Templates.Update(template);
}