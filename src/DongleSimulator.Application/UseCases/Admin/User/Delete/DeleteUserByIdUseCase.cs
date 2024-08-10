using DongleSimulator.Application.Extensions;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Domain.Services.ImageHost;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Sqids;

namespace DongleSimulator.Application.UseCases.Admin.User.Delete;

public class DeleteUserByIdUseCase : IDeleteUserByIdUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IStorageImageService _storageImageService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqids;

    public DeleteUserByIdUseCase(
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IStorageImageService storageImageService,
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqids
        )
    {
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _storageImageService = storageImageService;
        _unitOfWork = unitOfWork;
        _sqids = sqids;
    }
    
    public async Task Execute(string id)
    {
        var parsedId = _sqids.DecodeLong(id);

        var user = await _userWriteOnlyRepository.GetUserById(parsedId);
        if (user is null) throw new NotFoundException(ResourceExceptionMessages.USERNAME_DOES_NOT_EXISTS);

        foreach (var source in user.Sources) await _storageImageService.Delete(source.ImageIdentifier);
        foreach (var template in user.Templates) await _storageImageService.Delete(template.ImageIdentifier);
        
        await _userWriteOnlyRepository.Delete(parsedId);
        await _unitOfWork.Commit();
    }
}