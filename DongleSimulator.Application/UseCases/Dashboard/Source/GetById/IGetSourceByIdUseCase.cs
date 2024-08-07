using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.GetById;

public interface IGetSourceByIdUseCase
{
    public Task<ResponseSourceByIdJson> Execute(string id);
}