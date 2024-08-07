using DongleSimulator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DongleSimulator.Infra.Database;

public class DongleSimulatorDbContext : DbContext
{
    public DongleSimulatorDbContext(DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }
    public DbSet<Source> Sources { get; set; }
    public DbSet<Template> Templates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DongleSimulatorDbContext).Assembly);
    }
}