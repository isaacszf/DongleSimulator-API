using DongleSimulator.Domain.DTOs;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Requests;
using Shared.Responses;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.Filter;

public class FilterTemplatesUseCase : IFilterTemplatesUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public FilterTemplatesUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
    )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseTemplatesFromUserJson> Execute(RequestFilterTemplateJson req, int page, int pageSize)
    {
        var dto = new FilterTemplateDto
        {
            Username = req.Username,
            Title = req.Title,
            Replaces = req.Replaces,
            Status = req.Status.Distinct().Select(s => (Status)s).ToList(),
            OrderByMostRecent = req.OrderByMostRecent
        };
        
        var templates = await _dashboardReadOnlyRepository.FilterTemplates(dto, page, pageSize);

        return new ResponseTemplatesFromUserJson
        {
            Templates = templates.Items.Select(t => new ResponseSourceJson
            {
                Id = _sqids.Encode(t.Id),
                Title = t.Title,
                Subtitle = t.Subtitle,
                ImageUrl = _storageImageService.GetUrlByImageIdentifier(t.ImageIdentifier),
                Status = (Shared.Responses.Enums.Status) t.Status
            }).ToList(),
            Page = templates.Page,
            PageSize = templates.PageSize,
            TotalCount = templates.TotalCount
        };
    }
}