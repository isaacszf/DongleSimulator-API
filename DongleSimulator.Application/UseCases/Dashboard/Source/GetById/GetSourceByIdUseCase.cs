using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Repositories.Dashboard;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Responses;
using Shared.Responses.Enums;
using Sqids;

namespace DongleSimulator.Application.UseCases.Dashboard.Source.GetById;

public class GetSourceByIdUseCase : IGetSourceByIdUseCase
{
    private readonly IDashboardReadOnlyRepository _dashboardReadOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public GetSourceByIdUseCase(
        IDashboardReadOnlyRepository dashboardReadOnlyRepository,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
        )
    {
        _dashboardReadOnlyRepository = dashboardReadOnlyRepository;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseSourceByIdJson> Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);
        
        var source = await _dashboardReadOnlyRepository.GetSourceById(parsedId);
        if (source is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_SOURCE_ID);

        var url = _storageImageService.GetUrlByImageIdentifier(source.ImageIdentifier);
        
        return new ResponseSourceByIdJson
        {
            Id = _sqids.Encode(source.Id),
            Title = source.Title,
            Subtitle = source.Subtitle,
            ImageUrl = url,
            Status = (Status) source.Status,
            Username = source.User.Name,
            CreatedAt = source.CreatedAt
        };
    }
}