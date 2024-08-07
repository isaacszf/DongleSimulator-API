using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Responses;
using Shared.Responses.Enums;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Template.GetAll;

public class GetAllTemplatesUseCase : IGetAllTemplatesUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public GetAllTemplatesUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
    )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseTemplatesFromUserJson> Execute(int page, int pageSize)
    {
        var templates = await _dashboardReadOnlyRepository.GetAllTemplates(page, pageSize);
        
        return new ResponseTemplatesFromUserJson
        {
            Templates = templates.Items.Select(t =>
            {
                var url = _storageImageService.GetUrlByImageIdentifier(t.ImageIdentifier);
                
                return new ResponseSourceJson
                {
                    Id = _sqids.Encode(t.Id),
                    Title = t.Title,
                    Subtitle = t.Subtitle,
                    ImageUrl = url,
                    Status = (Status) t.Status,
                };
            }).ToList(),
            PageSize = templates.PageSize,
            Page = templates.Page
        };
    }
}