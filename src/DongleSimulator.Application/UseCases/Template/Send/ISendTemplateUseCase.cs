using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Template.Send;

public interface ISendTemplateUseCase
{
    public Task<ResponseImageRegisteredJson> Execute(RequestSendTemplateJson req);
}