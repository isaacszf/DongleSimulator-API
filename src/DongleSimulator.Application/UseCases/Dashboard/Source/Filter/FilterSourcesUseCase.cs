using DongleSimulator.Domain.DTOs;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Requests;
using Shared.Responses;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.Filter;

public class FilterSourcesUseCase : IFilterSourcesUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public FilterSourcesUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
        )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseSourcesFromUserJson> Execute(RequestFilterSourceJson req, int page, int pageSize)
    {
        var dto = new FilterSourceDto
        {
            Username = req.Username,
            Title = req.Title,
            Status = req.Status.Distinct().Select(s => (Status)s).ToList(),
            OrderByMostRecent = req.OrderByMostRecent
        };
        
        var sources = await _dashboardReadOnlyRepository.FilterSources(dto, page, pageSize);

        return new ResponseSourcesFromUserJson
        {
            Sources = sources.Items.Select(s => new ResponseSourceJson
            {
                Id = _sqids.Encode(s.Id),
                Title = s.Title,
                Subtitle = s.Subtitle,
                ImageUrl = _storageImageService.GetUrlByImageIdentifier(s.ImageIdentifier),
                Status = (Shared.Responses.Enums.Status) s.Status
            }).ToList(),
            Page = sources.Page,
            PageSize = sources.PageSize,
            TotalCount = sources.TotalCount
        };
    }
}