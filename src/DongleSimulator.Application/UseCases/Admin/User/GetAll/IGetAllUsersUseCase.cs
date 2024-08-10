using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Admin.User.GetAll;

public interface IGetAllUsersUseCase
{
    public Task<ResponsesUsersJson> Execute(int page, int pageSize);
}