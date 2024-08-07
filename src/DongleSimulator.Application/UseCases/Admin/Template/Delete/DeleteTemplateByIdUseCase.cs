using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Template;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.Template.Delete;

public class DeleteTemplateByIdUseCase : IDeleteTemplateByIdUseCase
{
    private readonly ITemplateReadOnlyRepository _templateReadOnlyRepository;
    private readonly ITemplateWriteOnlyRepository _templateWriteOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;

    public DeleteTemplateByIdUseCase(
        ITemplateReadOnlyRepository templateReadOnlyRepository,
        ITemplateWriteOnlyRepository templateWriteOnlyRepository,
        IStorageImageService storageImageService,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
        )
    {
        _templateReadOnlyRepository = templateReadOnlyRepository;
        _templateWriteOnlyRepository = templateWriteOnlyRepository;
        _storageImageService = storageImageService;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);

        var template = await _templateReadOnlyRepository.GetByIdAdminReadOnly(parsedId);
        if (template is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_TEMPLATE_ID);

        await _storageImageService.Delete(template.ImageIdentifier);
        await _templateWriteOnlyRepository.Delete(parsedId);
        await _unitOfWork.Commit();
    }
}