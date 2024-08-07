using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Responses;
using Shared.Responses.Enums;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.GetAll;

public class GetAllSourcesUseCase : IGetAllSourcesUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;
    
    public GetAllSourcesUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
        )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseSourcesFromUserJson> Execute(int page, int pageSize)
    {
        var sources = await _dashboardReadOnlyRepository.GetAllSources(page, pageSize);
        
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
                    Status = (Status) s.Status,
                };
            }).ToList(),
            PageSize = sources.PageSize,
            Page = sources.Page
        };
    }
}