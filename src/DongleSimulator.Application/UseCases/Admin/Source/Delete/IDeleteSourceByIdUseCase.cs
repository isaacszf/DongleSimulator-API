namespace DongleSimulator.Application.UseCases.Admin.Source.Delete;

public interface IDeleteSourceByIdUseCase
{
    public Task Execute(string id);
}