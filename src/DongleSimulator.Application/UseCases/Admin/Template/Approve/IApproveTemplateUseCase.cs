namespace DongleSimulator.Application.UseCases.Admin.Template.Approve;

public interface IApproveTemplateUseCase
{
    public Task Execute(string id);
}