using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Source;
using DongleSimulator.Domain.Services.ImageHost;
using DongleSimulator.Domain.Services.LoggedUser;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.UseCases.Source.Delete;

public class DeleteSourceByIdUseCase : IDeleteSourceByIdUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly ISourceReadOnlyRepository _sourceReadOnlyRepository;
    private readonly ISourceWriteOnlyRepository _sourceWriteOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;
    
    public DeleteSourceByIdUseCase(
        ILoggedUser loggedUser,
        ISourceReadOnlyRepository sourceReadOnlyRepository,
        ISourceWriteOnlyRepository sourceWriteOnlyRepository,
        IStorageImageService storageImageService,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
        )
    {
        _loggedUser = loggedUser;
        _sourceReadOnlyRepository = sourceReadOnlyRepository;
        _sourceWriteOnlyRepository = sourceWriteOnlyRepository;
        _storageImageService = storageImageService;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);

        var loggedUser = await _loggedUser.User();
        var source = await _sourceReadOnlyRepository.GetById(loggedUser, parsedId);

        if (source is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_SOURCE_ID);
        if (source.Status == Status.Approved) throw new ErrorOnValidation(ResourceExceptionMessages.CANNOT_DELETE_APPROVED);

        await _storageImageService.Delete(source.ImageIdentifier);
        await _sourceWriteOnlyRepository.Delete(parsedId);
        await _unitOfWork.Commit();
    }
}