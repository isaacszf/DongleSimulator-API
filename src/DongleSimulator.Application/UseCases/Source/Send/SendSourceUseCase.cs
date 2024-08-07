using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Source;
using DongleSimulator.Domain.Services.ImageHost;
using DongleSimulator.Domain.Services.LoggedUser;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.Source.Send;

public class SendSourceUseCase : ISendSourceUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly ISourceWriteOnlyRepository _sourceWriteOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly IUnitOfWork _unitOfWork;

    public SendSourceUseCase(
        ILoggedUser loggedUser,
        ISourceWriteOnlyRepository sourceWriteOnlyRepository,
        IStorageImageService storageImageService,
        IUnitOfWork unitOfWork
        )
    {
        _loggedUser = loggedUser;
        _sourceWriteOnlyRepository = sourceWriteOnlyRepository;
        _storageImageService = storageImageService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ResponseImageRegisteredJson> Execute(RequestSendImageJson req)
    {
        Validate(req);
        
        var loggedUser = await _loggedUser.User();
        
        var source = new Domain.Entities.Source
        {
            Title = req.Title,
            Subtitle = req.Subtitle,
            Status = Status.Pendent,
            
            UserId = loggedUser.Id,
        };

        var fileStream = req.Image.OpenReadStream();

        var (isValidImage, ext) = fileStream.ValidateAndGetImageExtension();
        if (!isValidImage) throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_IMAGE);
        
        source.ImageIdentifier = $"{loggedUser.Name}-source-{Guid.NewGuid()}{ext}";
        
        await _sourceWriteOnlyRepository.Create(source);
        await _unitOfWork.Commit();

        fileStream.Position = 0;
        await _storageImageService.Upload(fileStream, source.ImageIdentifier);
        
        return new ResponseImageRegisteredJson
        {
            Title = source.Title,
            Subtitle = source.Subtitle,
            ImageIdentifier = source.ImageIdentifier,
        };
    }

    private static void Validate(RequestSendImageJson req)
    {
        if (SourceTemplateValidator.IsTitleInvalid(req.Title))
            throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_TITLE);

        if (SourceTemplateValidator.IsSubtitleInvalid(req.Subtitle))
            throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_SUBTITLE);
    }
}