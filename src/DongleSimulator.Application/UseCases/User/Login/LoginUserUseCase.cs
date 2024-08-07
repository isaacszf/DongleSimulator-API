using DongleSimulator.Application.UseCases.User.Register;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Domain.Services.Cryptography;
using DongleSimulator.Domain.Services.Tokens;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Application.UseCases.User.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUserReadOnlyRepository _userRepository;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IAccessToken _accessToken;

    public LoginUserUseCase(
        IUserReadOnlyRepository userRepository,
        IPasswordEncoder passwordEncoder,
        IAccessToken accessToken
        )
    {
        _userRepository = userRepository;
        _passwordEncoder = passwordEncoder;
        _accessToken = accessToken;
    }
    
    public async Task<ResponseLoginJson> Execute(RequestLoginUserJson req)
    {
        Validate(req);

        var encryptedPassword = _passwordEncoder.Encrypt(req.Password);
        var user = await _userRepository.GetUserByEmailAndPassword(req.Email, encryptedPassword);

        if (user is null) throw new ErrorOnValidation(ResourceExceptionMessages.INVALID_CREDENTIALS);

        var token = _accessToken.Generate(user);
        
        return new ResponseLoginJson
        {
            Username = user.Name,
            Token = token
        };
    }

    private static void Validate(RequestLoginUserJson req)
    {
        var errors = new List<string>();
        
        if (!UserHelperValidator.IsEmailValid(req.Email)) errors.Add(ResourceExceptionMessages.INVALID_EMAIL);
        if (!UserHelperValidator.IsPasswordValid(req.Password)) errors.Add(ResourceExceptionMessages.INVALID_PASSWORD);
        
        if (errors.Count > 0) throw new ErrorOnValidation(errors);
    }
}