using System.Text.RegularExpressions;
using DongleSimulator.Domain.Enums;
using DongleSimulator.Domain.Repositories;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Domain.Services.Cryptography;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Requests;

namespace DongleSimulator.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IPasswordEncoder passwordEncoder,
        IUnitOfWork unitOfWork
        )
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _passwordEncoder = passwordEncoder;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(RequestRegisterUserJson req)
    {
        await Validate(req);

        var encryptedPassword = _passwordEncoder.Encrypt(req.Password);
            
        var user = new Domain.Entities.User
        {
            Email = req.Email,
            Name = req.Name,
            Password = encryptedPassword,
            Role = UserRole.Default,
            UserIdentifier = Guid.NewGuid()
        };

        await _userWriteOnlyRepository.Create(user);
        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestRegisterUserJson req)
    {
        var errors = new List<string>();
        
        if (!UserHelperValidator.IsEmailValid(req.Email)) errors.Add(ResourceExceptionMessages.INVALID_EMAIL);
        if (!UserHelperValidator.IsUsernameValid(req.Name)) errors.Add(ResourceExceptionMessages.INVALID_USERNAME);
        if (!UserHelperValidator.IsPasswordValid(req.Password)) errors.Add(ResourceExceptionMessages.INVALID_PASSWORD);

        var parsedName = Regex.Replace(req.Name.ToLower(), @"\s+", "");
        var usernameInUse = await _userReadOnlyRepository.ExistsUserWithUsername(parsedName);
        
        if (usernameInUse) errors.Add(ResourceExceptionMessages.USERNAME_ALREADY_EXISTS);

        var parsedEmail = req.Email.ToLower();
        var emailInUse = await _userReadOnlyRepository.ExistsUserWithEmail(parsedEmail);
        
        if (emailInUse) errors.Add(ResourceExceptionMessages.EMAIL_ALREADY_EXISTS);
        
        if (errors.Count > 0) throw new ErrorOnValidation(errors);
    }
}