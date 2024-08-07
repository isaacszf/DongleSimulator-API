using DongleSimulator.Application.UseCases.Admin.Generate;
using DongleSimulator.Application.UseCases.Admin.Source.Approve;
using DongleSimulator.Application.UseCases.Admin.Source.Deny;
using DongleSimulator.Application.UseCases.Admin.Template.Approve;
using DongleSimulator.Application.UseCases.Admin.Template.Delete;
using DongleSimulator.Application.UseCases.Admin.Template.Deny;
using DongleSimulator.Application.UseCases.Dashboard.Source.GetAll;
using DongleSimulator.Application.UseCases.Dashboard.Source.GetAllByUsername;
using DongleSimulator.Application.UseCases.Dashboard.Source.GetById;
using DongleSimulator.Application.UseCases.Dashboard.Template.GetAll;
using DongleSimulator.Application.UseCases.Source.Delete;
using DongleSimulator.Application.UseCases.Source.Send;
using DongleSimulator.Application.UseCases.Template.Delete;
using DongleSimulator.Application.UseCases.Template.Send;
using DongleSimulator.Application.UseCases.User.Login;
using DongleSimulator.Application.UseCases.User.Profile;
using DongleSimulator.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;
using DeleteTemplateByIdUseCase = DongleSimulator.Application.UseCases.Admin.Template.Delete.DeleteTemplateByIdUseCase;
using IDeleteTemplateByIdUseCase = DongleSimulator.Application.UseCases.Admin.Template.Delete.IDeleteTemplateByIdUseCase;

namespace DongleSimulator.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration config)
    {
        AddUseCases(services);
        AddSqids(services, config);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // User
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        
        services.AddScoped<IGetProfileUseCase, GetProfileUseCase>();
        
        // Sources
        services.AddScoped<ISendSourceUseCase, SendSourceUseCase>();
        services.AddScoped<IDeleteSourceByIdUseCase, DeleteSourceByIdUseCase>();

        // Templates
        services.AddScoped<ISendTemplateUseCase, SendTemplateUseCase>();
        services.AddScoped<UseCases.Template.Delete.IDeleteTemplateByIdUseCase, UseCases.Template.Delete.DeleteTemplateByIdUseCase>();
        
        // Dashboard
        services.AddScoped<IGetAllSourcesUseCase, GetAllSourcesUseCase>();
        services.AddScoped<IGetAllSourcesByUserUseCase, GetAllSourcesByUserUseCase>();
        services.AddScoped<IGetSourceByIdUseCase, GetSourceByIdUseCase>();
        services.AddScoped<IGetAllTemplatesUseCase, GetAllTemplatesUseCase>();
        
        // Admin
        services.AddScoped<IApproveSourceUseCase, ApproveSourceUseCase>();
        services.AddScoped<IDenySourceUseCase, DenySourceUseCase>();
        services.AddScoped<IApproveTemplateUseCase, ApproveTemplateUseCase>();
        services.AddScoped<IDenyTemplateUseCase, DenyTemplateUseCase>();
        services.AddScoped<UseCases.Admin.Source.Delete.IDeleteSourceByIdUseCase, UseCases.Admin.Source.Delete.DeleteSourceByIdUseCase>();
        services.AddScoped<IDeleteTemplateByIdUseCase, DeleteTemplateByIdUseCase>();
        services.AddScoped<IGenerateImageUseCase, GenerateImageUseCase>();
    }

    private static void AddSqids(IServiceCollection services, IConfiguration config)
    {
        var alphabet = config.GetSection("Sqids:Alphabet").Value!;
        
        services.AddSingleton(new SqidsEncoder<long>(new SqidsOptions
        {
            Alphabet = alphabet,
            MinLength = 5
        }));
    }
}