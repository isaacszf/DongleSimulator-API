using Shared.Requests;

namespace DongleSimulator.Application.UseCases.User.Register;

public interface IRegisterUserUseCase
{
    public Task Execute(RequestRegisterUserJson req);
}