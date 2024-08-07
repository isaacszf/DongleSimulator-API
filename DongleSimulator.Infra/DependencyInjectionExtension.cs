using CloudinaryDotNet;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Domain.Repositories.Source;
using DongleSimulator.Domain.Repositories.Template;
using DongleSimulator.Domain.Services.Cryptography;
using DongleSimulator.Domain.Services.Generator;
using DongleSimulator.Domain.Services.ImageHost;
using DongleSimulator.Domain.Services.LoggedUser;
using DongleSimulator.Domain.Services.Tokens;
using DongleSimulator.Infra.Database;
using DongleSimulator.Infra.Repositories;
using DongleSimulator.Infra.Services.Cryptography;
using DongleSimulator.Infra.Services.Generator;
using DongleSimulator.Infra.Services.ImageHost;
using DongleSimulator.Infra.Services.LoggedUser;
using DongleSimulator.Infra.Services.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DongleSimulator.Infra;

public static class DependencyInjectionExtension
{
    public static void AddInfra(this IServiceCollection services, IConfiguration config)
    {
        AddDatabase(services, config);
        AddRepos(services);

        AddPasswordEncoder(services, config);
        AddTokens(services, config);
        AddCloudinary(services, config);
        AddAccessToLoggedUser(services);
        AddImageGenerator(services);
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration config)
    {
        var connection = config.GetSection("Database:Connection").Value!;
        services.AddDbContext<DongleSimulatorDbContext>(options => options.UseNpgsql(connection));
    }

    private static void AddRepos(IServiceCollection services)
    {
        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // User
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        
        // Source
        services.AddScoped<ISourceWriteOnlyRepository, SourceRepository>();
        services.AddScoped<ISourceReadOnlyRepository, SourceRepository>();
        services.AddScoped<ISourceUpdateOnlyRepository, SourceRepository>();
        
        // Template
        services.AddScoped<ITemplateWriteOnlyRepository, TemplateRepository>();
        services.AddScoped<ITemplateReadOnlyRepository, TemplateRepository>();
        services.AddScoped<ITemplateUpdateOnlyRepository, TemplateRepository>();
        
        // Dashboard
        services.AddScoped<IDashboardReadOnlyRepository, DashboardRepository>();
    }
    
    private static void AddPasswordEncoder(IServiceCollection services, IConfiguration config)
    {
        var key = config.GetSection("Encoder:Key").Value!;
        services.AddScoped<IPasswordEncoder>(_ => new PasswordEncoder(key));
    }

    private static void AddTokens(IServiceCollection services, IConfiguration config)
    {
        var key = config.GetSection("JWT:Secret").Value!;
        var expirationTime = int.Parse(config.GetSection("JWT:ExpirationTime").Value!);

        services.AddScoped<IAccessToken>(_ => new JwtTokenService(expirationTime, key));
    }

    private static void AddCloudinary(IServiceCollection services, IConfiguration config)
    {
        var url = config.GetSection("Cloudinary:URL").Value!;
        var client = new Cloudinary(url);
        
        services.AddScoped<IStorageImageService>(_ => new CloudinaryHostService(client));
    }

    private static void AddImageGenerator(IServiceCollection services)
    {
        var http = new HttpClient();
        services.AddScoped<IImageGeneratorService>(_ => new SixLaborsImageGeneratorService(http));
    }
    
    private static void AddAccessToLoggedUser(IServiceCollection services) 
        => services.AddScoped<ILoggedUser, LoggedUser>();
}