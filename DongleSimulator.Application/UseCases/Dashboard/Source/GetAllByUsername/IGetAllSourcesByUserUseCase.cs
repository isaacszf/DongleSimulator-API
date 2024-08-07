using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.GetAllByUsername;

public interface IGetAllSourcesByUserUseCase
{
    public Task<ResponseSourcesFromUserJson> Execute(string username, int page, int pageSize);
}