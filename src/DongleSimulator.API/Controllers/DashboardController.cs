using DongleSimulator.Application.UseCases.Dashboard.Source.Filter;
using DongleSimulator.Application.UseCases.Dashboard.Source.GetById;
using DongleSimulator.Application.UseCases.Dashboard.Template.Filter;
using DongleSimulator.Application.UseCases.Dashboard.Template.GetById;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    [HttpPost("sources")]
    [ProducesResponseType(typeof(ResponseSourcesFromUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> FilterSources(
        [FromServices] IFilterSourcesUseCase useCase,
        [FromBody] RequestFilterSourceJson req,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var res = await useCase.Execute(req, page, pageSize);
        return Ok(res);
    }
    
    [HttpGet("sources/{id}")]
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
    
    [HttpPost("templates")]
    [ProducesResponseType(typeof(ResponseSourcesFromUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> FilterTemplates(
        [FromServices] IFilterTemplatesUseCase useCase,
        [FromBody] RequestFilterTemplateJson req,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var res = await useCase.Execute(req, page, pageSize);
        return Ok(res);
    }
    
    [HttpGet("templates/{id}")]
    [ProducesResponseType(typeof(ResponseSourcesFromUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTemplateById(
        [FromServices] IGetTemplateByIdUseCase useCase,
        [FromRoute] string id
    )
    {
        var res = await useCase.Execute(id);
        return Ok(res);
    }
}