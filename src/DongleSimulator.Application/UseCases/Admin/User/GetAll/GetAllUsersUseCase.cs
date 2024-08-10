using DongleSimulator.Domain.Repositories.Dashboard;
using Shared.Responses;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.User.GetAll;

public class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly SqidsEncoder<long> _sqids;

    public GetAllUsersUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        SqidsEncoder<long> sqids
        )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _sqids = sqids;
    }
    
    public async Task<ResponsesUsersJson> Execute(int page, int pageSize)
    {
        var users = await _dashboardReadOnlyRepository.GetAllUsers(page, pageSize);

        return new ResponsesUsersJson
        {
            Users = users.Items.Select(u => new ResponseUserJson
            {
                Id = _sqids.Encode(u.Id),
                Username = u.Name,
                Email = u.Email,
                UserIdentifier = u.UserIdentifier,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList(),
            Page = page,
            PageSize = pageSize
        };
    }
}