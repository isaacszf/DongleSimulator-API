using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Admin.Generate;

public interface IGenerateImageUseCase
{
    public Task<ResponseGenerateImageJson> Execute(RequestGenerateImageJson? req, bool random);
}