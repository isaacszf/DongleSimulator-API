namespace DongleSimulator.Application.UseCases.Admin.Source.Deny;

public interface IDenySourceUseCase
{
    public Task Execute(string id);
}