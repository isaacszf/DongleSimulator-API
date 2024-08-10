using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.Filter;

public interface IFilterSourcesUseCase
{
    public Task<ResponseSourcesFromUserJson> Execute(RequestFilterSourceJson req, int page, int pageSize);
}