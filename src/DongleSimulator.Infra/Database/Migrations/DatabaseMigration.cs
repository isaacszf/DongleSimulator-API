using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DongleSimulator.Infra.Database.Migrations;

public static class DatabaseMigration
{
    public static async Task MigrateDb(IConfiguration config)
    {
        var connection = config.GetSection("Database:Connection").Value!;
        var schemaName = config.GetSection("Database:DefaultSchema").Value!;
        
        var builder = new NpgsqlConnectionStringBuilder(connection);
        var query = $"CREATE SCHEMA IF NOT EXISTS {schemaName}";

        await using var dataSource = NpgsqlDataSource.Create(builder.ConnectionString);
        await using var cmdCreateDb = dataSource.CreateCommand(query);
        await cmdCreateDb.ExecuteNonQueryAsync();
    }
    
    public static async Task MigrateTables(IConfiguration config)
    {
        var connection = config.GetSection("Database:Connection").Value!;
        var schemaName = config.GetSection("Database:DefaultSchema").Value!;
    
        var builder = new DbContextOptionsBuilder<DongleSimulatorDbContext>();
        builder.UseNpgsql(connection, opts =>
        {
            opts.MigrationsHistoryTable(HistoryRepository.DefaultTableName, schemaName);
        });

        await using var context = new DongleSimulatorDbContext(builder.Options);
        await context.Database.MigrateAsync();
    }
}