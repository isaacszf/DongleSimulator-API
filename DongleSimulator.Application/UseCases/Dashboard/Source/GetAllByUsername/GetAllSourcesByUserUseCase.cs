using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Responses;
using Shared.Responses.Enums;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.GetAllByUsername;

public class GetAllSourcesByUserUseCase : IGetAllSourcesByUserUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public GetAllSourcesByUserUseCase(
        IUserReadOnlyRepository userReadOnlyRepository, 
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
        )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseSourcesFromUserJson> Execute(string username, int page, int pageSize)
    {
        var userExists = await _userReadOnlyRepository.ExistsUserWithUsername(username.ToLower());
        if (!userExists) throw new NotFoundException(ResourceExceptionMessages.USERNAME_DOES_NOT_EXISTS);

        var sources = 
            await _dashboardReadOnlyRepository.GetAllSourcesByUsername(username, page, pageSize);

        return new ResponseSourcesFromUserJson
        {
            Sources = sources.Items.Select(s =>
            {
                var url = _storageImageService.GetUrlByImageIdentifier(s.ImageIdentifier);
                
                return new ResponseSourceJson
                {
                    Id = _sqids.Encode(s.Id),
                    Title = s.Title,
                    Subtitle = s.Subtitle,
                    ImageUrl = url,
                    Status = (Status)s.Status,
                };
            }).ToList(),
            PageSize = sources.PageSize,
            Page = sources.Page,
        };
    }
}