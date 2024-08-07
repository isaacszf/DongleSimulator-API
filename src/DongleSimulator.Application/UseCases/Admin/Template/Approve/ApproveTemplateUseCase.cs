using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.Template;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.Template.Approve;

public class ApproveTemplateUseCase : IApproveTemplateUseCase
{
    private readonly ITemplateUpdateOnlyRepository _templateUpdateOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;
    
    public ApproveTemplateUseCase(
        ITemplateUpdateOnlyRepository templateUpdateOnlyRepository,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
    )
    {
        _templateUpdateOnlyRepository = templateUpdateOnlyRepository;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);

        var template = await _templateUpdateOnlyRepository.GetByIdAdminUpdateOnly(parsedId);
        if (template is null) throw new NotFoundException(ResourceExceptionMessages.INVALID_TEMPLATE_ID);

        if (template.Status == Status.Approved) 
            throw new ErrorOnValidation(ResourceExceptionMessages.STATUS_IS_ALREADY);
        
        template.Status = Status.Approved;
        
        _templateUpdateOnlyRepository.Update(template);
        await _unitOfWork.Commit();
    }
}