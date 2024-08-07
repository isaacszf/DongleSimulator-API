namespace DongleSimulator.Application.UseCases.Admin.Source.Approve;

public interface IApproveSourceUseCase
{
    public Task Execute(string id);
}