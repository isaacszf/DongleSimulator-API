using DongleSimulator.Application.UseCases.Source.Delete;
using DongleSimulator.Application.UseCases.Source.Send;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Controllers;

[ApiController]
[Route("api/source")]
[Authorize]
public class SourcesController : ControllerBase
{
    [HttpPost("create")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseImageRegisteredJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Send(
            [FromServices] ISendSourceUseCase useCase,
            [FromForm] RequestSendImageJson req
        )
    {
        var res = await useCase.Execute(req);
        return Created(string.Empty, res);
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteById(
            [FromServices] IDeleteSourceByIdUseCase useCase,
            [FromRoute] string id
        )
    {
        await useCase.Execute(id);
        return NoContent();
    }
}