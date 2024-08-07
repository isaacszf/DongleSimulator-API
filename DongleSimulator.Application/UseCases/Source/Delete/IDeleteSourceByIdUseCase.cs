namespace DongleSimulator.Application.UseCases.Source.Delete;

public interface IDeleteSourceByIdUseCase
{
    public Task Execute(string id);
}