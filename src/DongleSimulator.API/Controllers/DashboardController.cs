using DongleSimulator.Application.UseCases.Dashboard.Source.GetAll;
using DongleSimulator.Application.UseCases.Dashboard.Source.GetAllByUsername;
using DongleSimulator.Application.UseCases.Dashboard.Source.GetById;
using DongleSimulator.Application.UseCases.Dashboard.Template.GetAll;
using Microsoft.AspNetCore.Mvc;
using Shared.Responses;

namespace DongleSimulator.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    [HttpGet("sources")]
    [ProducesResponseType(typeof(ResponseSourcesFromUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllSources(
        [FromServices] IGetAllSourcesUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var res = await useCase.Execute(page, pageSize);
        return Ok(res);
    }
    
    [HttpGet("templates")]
    [ProducesResponseType(typeof(ResponseTemplatesFromUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTemplates(
        [FromServices] IGetAllTemplatesUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var res = await useCase.Execute(page, pageSize);
        return Ok(res);
    }
    
    [HttpGet("sources/user/{username}")]
    [ProducesResponseType(typeof(ResponseSourcesFromUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByUsername(
        [FromServices] IGetAllSourcesByUserUseCase useCase,
        [FromRoute] string username,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var res = await useCase.Execute(username, page, pageSize);
        return Ok(res);
    }
    
    [HttpGet("sources/info/{id}")]
    [ProducesResponseType(typeof(ResponseSourcesFromUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSourceById(
        [FromServices] IGetSourceByIdUseCase useCase,
        [FromRoute] string id
    )
    {
        var res = await useCase.Execute(id);
        return Ok(res);
    }
}