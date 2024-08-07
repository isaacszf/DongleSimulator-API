namespace DongleSimulator.Application.UseCases.Admin.Template.Deny;

public interface IDenyTemplateUseCase
{
    public Task Execute(string id);
}