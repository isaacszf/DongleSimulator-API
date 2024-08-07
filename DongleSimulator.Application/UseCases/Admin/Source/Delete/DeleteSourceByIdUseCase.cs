using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Source;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.Source.Delete;

public class DeleteSourceByIdUseCase : IDeleteSourceByIdUseCase
{
    private readonly ISourceReadOnlyRepository _sourceReadOnlyRepository;
    private readonly ISourceWriteOnlyRepository _sourceWriteOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;
    
    public DeleteSourceByIdUseCase(
        ISourceReadOnlyRepository sourceReadOnlyRepository,
        ISourceWriteOnlyRepository sourceWriteOnlyRepository,
        IStorageImageService storageImageService,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
        )
    {
        _sourceReadOnlyRepository = sourceReadOnlyRepository;
        _sourceWriteOnlyRepository = sourceWriteOnlyRepository;
        _storageImageService = storageImageService;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);

        var source = await _sourceReadOnlyRepository.GetByIdAdminReadOnly(parsedId);
        if (source is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_SOURCE_ID);

        await _storageImageService.Delete(source.ImageIdentifier);
        await _sourceWriteOnlyRepository.Delete(parsedId);
        await _unitOfWork.Commit();
    }
}