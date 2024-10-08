using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Template;
using DongleSimulator.Domain.Services.ImageHost;
using DongleSimulator.Domain.Services.LoggedUser;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Requests;
using Shared.Responses;
using Sqids;

namespace DongleSimulator.Application.UseCases.Template.Send;

public class SendTemplateUseCase : ISendTemplateUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly ITemplateWriteOnlyRepository _templateWriteOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;

    public SendTemplateUseCase(
        ILoggedUser loggedUser,
        ITemplateWriteOnlyRepository templateWriteOnlyRepository,
        IStorageImageService storageImageService,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
    )
    {
        _loggedUser = loggedUser;
        _templateWriteOnlyRepository = templateWriteOnlyRepository;
        _storageImageService = storageImageService;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task<ResponseImageRegisteredJson> Execute(RequestSendTemplateJson req)
    {
        Validate(req);
        
        var loggedUser = await _loggedUser.User();
        
        var template = new Domain.Entities.Template
        {
            Title = req.Title,
            Subtitle = req.Subtitle,
            Status = Status.Pendent,
            Replaces = req.Replaces,
            
            UserId = loggedUser.Id,
        };

        var fileStream = req.Image.OpenReadStream();

        var (isValidImage, ext) = fileStream.ValidateAndGetImageExtension();
        if (!isValidImage) throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_IMAGE);
        
        template.ImageIdentifier = $"{loggedUser.Name}-template-{Guid.NewGuid()}{ext}";
        
        await _templateWriteOnlyRepository.Create(template);
        await _unitOfWork.Commit();

        fileStream.Position = 0;
        await _storageImageService.Upload(fileStream, template.ImageIdentifier);
        
        return new ResponseImageRegisteredJson
        {
            Id = _sqids.Encode(template.Id),
            Title = template.Title,
            Subtitle = template.Subtitle,
            ImageIdentifier = template.ImageIdentifier,
        };
    }

    private static void Validate(RequestSendTemplateJson req)
    {
        if (SourceTemplateValidator.IsTitleInvalid(req.Title))
            throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_TITLE);

        if (SourceTemplateValidator.IsSubtitleInvalid(req.Subtitle))
            throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_SUBTITLE);

        if (req.Replaces is < 1 or > 2)
            throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_REPLACES);
    }
}