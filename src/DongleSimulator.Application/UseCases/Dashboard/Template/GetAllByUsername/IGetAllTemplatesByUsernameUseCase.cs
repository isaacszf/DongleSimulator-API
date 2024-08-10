using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.GetAllByUsername;

public interface IGetAllTemplatesByUsernameUseCase
{
    public Task<ResponseTemplatesFromUserJson> Execute(string username, int page, int pageSize);
}