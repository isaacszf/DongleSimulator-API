using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.GetAll;

public interface IGetAllSourcesUseCase
{
    public Task<ResponseSourcesFromUserJson> Execute(int page, int pageSize);
}