namespace DongleSimulator.Application.UseCases.Admin.Template.Delete;

public interface IDeleteTemplateByIdUseCase
{
    public Task Execute(string id);
}