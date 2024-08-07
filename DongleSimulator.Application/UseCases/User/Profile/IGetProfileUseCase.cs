using Shared.Responses;

namespace DongleSimulator.Application.UseCases.User.Profile;

public interface IGetProfileUseCase
{
    public Task<ResponseUserProfileJson> Execute();
}