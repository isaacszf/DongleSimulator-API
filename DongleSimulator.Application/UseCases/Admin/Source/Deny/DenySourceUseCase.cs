using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Source;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.Source.Deny;

public class DenySourceUseCase : IDenySourceUseCase
{
    private readonly ISourceUpdateOnlyRepository _sourceUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;
    
    public DenySourceUseCase(
        ISourceUpdateOnlyRepository sourceUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
    )
    {
        _sourceUpdateOnlyRepository = sourceUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);

        var source = await _sourceUpdateOnlyRepository.GetByIdAdminUpdateOnly(parsedId);
        if (source is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_SOURCE_ID);

        if (source.Status == Status.Denied) 
            throw new ErrorOnValidation(ResourceExceptionMessages.STATUS_IS_ALREADY);
        
        source.Status = Status.Denied;
        
        _sourceUpdateOnlyRepository.Update(source);
        await _unitOfWork.Commit();
    }
}