using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.User.Login;

public interface ILoginUserUseCase
{
    public Task<ResponseLoginJson> Execute(RequestLoginUserJson req);
}