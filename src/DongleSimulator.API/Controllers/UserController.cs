using DongleSimulator.Application.UseCases.User.Login;
using DongleSimulator.Application.UseCases.User.Profile;
using DongleSimulator.Application.UseCases.User.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Profile([FromServices] IGetProfileUseCase useCase)
    {
        var res = await useCase.Execute();
        return Ok(res);
    }
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
            [FromBody] RequestRegisterUserJson req,
            [FromServices] IRegisterUserUseCase useCase
        )
    {
        await useCase.Execute(req);
        return Created(string.Empty, new { created = true });
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
            [FromBody] RequestLoginUserJson req,
            [FromServices] ILoginUserUseCase useCase
        )
    {
        var res = await useCase.Execute(req);
        return Ok(res);
    }
}