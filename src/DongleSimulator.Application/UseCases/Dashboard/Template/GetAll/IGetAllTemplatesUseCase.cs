using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.GetAll;

public interface IGetAllTemplatesUseCase
{
    public Task<ResponseTemplatesFromUserJson> Execute(int page, int pageSize);
}