using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories.Source;
using DongleSimulator.Domain.Repositories.Template;
using DongleSimulator.Domain.Services.Generator;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Requests;
using Shared.Responses;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.Generate;

public class GenerateImageUseCase : IGenerateImageUseCase
{
    private const string OutputImagePath = "generated_image.png";
    
    private readonly ISourceReadOnlyRepository _sourceReadOnlyRepository;
    private readonly ITemplateReadOnlyRepository _templateReadOnlyRepository;
    private readonly IImageGeneratorService _imageGeneratorService;
    private readonly IStorageImageService _storageImageService;
    private readonly SqidsEncoder<long> _sqids;

    public GenerateImageUseCase(
        ISourceReadOnlyRepository sourceReadOnlyRepository,
        ITemplateReadOnlyRepository templateReadOnlyRepository,
        IImageGeneratorService imageGeneratorService,
        IStorageImageService storageImageService,
        SqidsEncoder<long> sqids
        )
    {
        _sourceReadOnlyRepository = sourceReadOnlyRepository;
        _templateReadOnlyRepository = templateReadOnlyRepository;
        _imageGeneratorService = imageGeneratorService;
        _storageImageService = storageImageService;
        _sqids = sqids;
    }
    
    public async Task<ResponseGenerateImageJson> Execute(RequestGenerateImageJson? req, bool random)
    {
        if (random) return await GenerateRandom();
        
        if (req is null) throw new ErrorOnValidation(ResourceExceptionMessages.MISSING_BODY);
        return await GenerateByReq(req);
    }

    private async Task<ResponseGenerateImageJson> GenerateRandom()
    {
        var template = await _templateReadOnlyRepository.GetRandomApproved();
        if (template is null) throw new NotFoundException(ResourceExceptionMessages.EMPTY_ITEM);
        
        var sources = new List<Domain.Entities.Source>();

        for (var i = 0; i < template.Replaces; i++)
        {
            var source = await _sourceReadOnlyRepository.GetRandomApproved();
            if (source is null) throw new NotFoundException(ResourceExceptionMessages.EMPTY_ITEM);
            
            sources.Add(source);
        }
        
        return await ReturnResponse(sources, template);
    }
    
    private async Task<ResponseGenerateImageJson> GenerateByReq(RequestGenerateImageJson req)
    {
        var sourceIds = req.SourcesIds.Select(s => _sqids.DecodeLong(s)).ToList();
        var templateId = _sqids.DecodeLong(req.TemplateId);
        
        var template = await _templateReadOnlyRepository.GetByIdAdminReadOnly(templateId);
        if (template is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_TEMPLATE_ID);

        if (req.SourcesIds.Count != template.Replaces)
            throw new ErrorOnValidation(ResourceExceptionMessages.NOT_ENOUGH_SOURCES);

        var sources = new List<Domain.Entities.Source>();
        for (var i = 0; i < template.Replaces; i++)
        {
            var source = await _sourceReadOnlyRepository.GetByIdAdminReadOnly(sourceIds[i]);
                
            if (source is null) 
                throw new NotFoundException(ResourceExceptionMessages.INVALID_SOURCE_ID);
            
            sources.Add(source);
        }

        if (sources.Any(s => s.Status != Status.Approved)) 
            throw new ErrorOnValidation(ResourceExceptionMessages.SOURCE_NOT_APPROVED);
        
        if (template.Status != Status.Approved) 
            throw new ErrorOnValidation(ResourceExceptionMessages.TEMPLATE_NOT_APPROVED);
        
        return await ReturnResponse(sources, template);
    }

    private async Task<ResponseGenerateImageJson> ReturnResponse(
        IList<Domain.Entities.Source> sources, 
        Domain.Entities.Template template
        )
    {
        var sourcesUrls = sources
            .Select(source => _storageImageService.GetUrlByImageIdentifier(source.ImageIdentifier))
            .ToList();
        
        var templateUrl = _storageImageService.GetUrlByImageIdentifier(template.ImageIdentifier);
        
        var outputStream = await _imageGeneratorService.Generate(sourcesUrls, templateUrl, template.Replaces);
        await _storageImageService.Upload(outputStream, OutputImagePath);
        
        return new ResponseGenerateImageJson
        {
            OutputUrl = _storageImageService.GetUrlByImageIdentifier(OutputImagePath),
        };
    }
}