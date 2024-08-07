using DongleSimulator.Domain.Repositories;
using DongleSimulator.Infra.Database;

namespace DongleSimulator.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DongleSimulatorDbContext _dbContext;

    public UnitOfWork(DongleSimulatorDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}