using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Responses;
using Shared.Responses.Enums;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.GetById;

public class GetTemplateByIdUseCase : IGetTemplateByIdUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public GetTemplateByIdUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
    )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseTemplateByIdJson> Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);
        
        var template = await _dashboardReadOnlyRepository.GetTemplateById(parsedId);
        if (template is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_TEMPLATE_ID);

        var url = _storageImageService.GetUrlByImageIdentifier(template.ImageIdentifier);
        
        return new ResponseTemplateByIdJson
        {
            Id = _sqids.Encode(template.Id),
            Title = template.Title,
            Subtitle = template.Subtitle,
            Replaces = template.Replaces,
            ImageUrl = url,
            Status = (Status) template.Status,
            Username = template.User.Name,
            CreatedAt = template.CreatedAt
        };
    }
}