namespace DongleSimulator.Application.UseCases.Template.Delete;

public interface IDeleteTemplateByIdUseCase
{
    public Task Execute(string id);
}