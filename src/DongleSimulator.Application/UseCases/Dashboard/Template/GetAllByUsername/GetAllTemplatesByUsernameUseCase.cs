using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Responses;
using Shared.Responses.Enums;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.GetAllByUsername;

public class GetAllTemplatesByUsernameUseCase : IGetAllTemplatesByUsernameUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public GetAllTemplatesByUsernameUseCase(
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
    
    public async Task<ResponseTemplatesFromUserJson> Execute(string username, int page, int pageSize)
    {
        var userExists = await _userReadOnlyRepository.ExistsUserWithUsername(username);
        if (!userExists) throw new NotFoundException(ResourceExceptionMessages.USERNAME_DOES_NOT_EXISTS);

        var templates = 
            await _dashboardReadOnlyRepository.GetAllTemplatesByUsername(username, page, pageSize);
        
        return new ResponseTemplatesFromUserJson
        {
            Templates = templates.Items.Select(t => new ResponseSourceJson
            {
                Id = _sqids.Encode(t.Id),
                Title = t.Title,
                Subtitle = t.Subtitle,
                ImageUrl = _storageImageService.GetUrlByImageIdentifier(t.ImageIdentifier),
                Status = (Status) t.Status
            }).ToList(),
            PageSize = templates.PageSize,
            Page = templates.Page
        };
    }
}