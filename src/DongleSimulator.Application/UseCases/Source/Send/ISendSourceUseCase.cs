using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Source.Send;

public interface ISendSourceUseCase
{
    public Task<ResponseImageRegisteredJson> Execute(RequestSendImageJson req);
}