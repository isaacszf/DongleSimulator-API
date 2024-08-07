using DongleSimulator.Application.UseCases.Template.Delete;
using DongleSimulator.Application.UseCases.Template.Send;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Controllers;

[ApiController]
[Route("api/template")]
[Authorize]
public class TemplateController : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseImageRegisteredJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Send(
        [FromServices] ISendTemplateUseCase useCase,
        [FromForm] RequestSendTemplateJson req
    )
    {
        var res = await useCase.Execute(req);
        return Created(string.Empty, res);
    }
    
    [HttpDelete("delete/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(
        [FromServices] IDeleteTemplateByIdUseCase useCase,
        [FromRoute] string id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }
}