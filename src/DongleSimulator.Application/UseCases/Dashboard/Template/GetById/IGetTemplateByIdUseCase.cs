using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.GetById;

public interface IGetTemplateByIdUseCase
{
    public Task<ResponseTemplateByIdJson> Execute(string id);
}