using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.Filter;

public interface IFilterTemplatesUseCase
{
    public Task<ResponseTemplatesFromUserJson> Execute(RequestFilterTemplateJson req, int page, int pageSize);
}