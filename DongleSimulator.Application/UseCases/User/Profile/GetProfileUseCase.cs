using DongleSimulator.Domain.Services.LoggedUser;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.User.Profile;

public class GetProfileUseCase : IGetProfileUseCase
{
    private readonly ILoggedUser _loggedUser;

    public GetProfileUseCase(ILoggedUser loggedUser) => _loggedUser = loggedUser; 
    
    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.User();

        return new ResponseUserProfileJson
        {
            Name = user.Name,
            Email = user.Email
        };
    }
}