using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories.Source;
using DongleSimulator.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DongleSimulator.Infra.Repositories;

public class SourceRepository : ISourceWriteOnlyRepository, ISourceReadOnlyRepository, ISourceUpdateOnlyRepository
{
    private readonly DongleSimulatorDbContext _dbContext;

    public SourceRepository(DongleSimulatorDbContext dbContext) => _dbContext = dbContext;

    public async Task Create(Source source) => await _dbContext.Sources.AddAsync(source);

    public async Task<Source?> GetById(User user, long id)
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id.Equals(id) && s.UserId.Equals(user.Id));
    }
    
    public async Task<Source?> GetByIdAdminReadOnly(long id)
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id.Equals(id));
    }

    public async Task<Source?> GetByIdAdminUpdateOnly(long id)
    {
        return await _dbContext.Sources
            .FirstOrDefaultAsync(s => s.Id.Equals(id));
    }
    
    public async Task<Source?> GetRandomApproved()
    {
        return await _dbContext.Sources
            .AsNoTracking()
            .Where(s => s.Status == Status.Approved)
            .OrderBy(_ => Guid.NewGuid())
            .FirstOrDefaultAsync();
    }
    
    public async Task Delete(long id)
    {
        var source = await _dbContext.Sources.FindAsync(id);
        _dbContext.Sources.Remove(source!);
    }

    public void Update(Source source) => _dbContext.Sources.Update(source);
}