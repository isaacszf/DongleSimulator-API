using DongleSimulator.Application.UseCases.Admin.Generate;
using DongleSimulator.Application.UseCases.Admin.Source.Approve;
using DongleSimulator.Application.UseCases.Admin.Source.Delete;
using DongleSimulator.Application.UseCases.Admin.Source.Deny;
using DongleSimulator.Application.UseCases.Admin.Template.Approve;
using DongleSimulator.Application.UseCases.Admin.Template.Delete;
using DongleSimulator.Application.UseCases.Admin.Template.Deny;
using DongleSimulator.Application.UseCases.Admin.User.Delete;
using DongleSimulator.Application.UseCases.Admin.User.GetAll;
using DongleSimulator.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Requests;
using Shared.Responses;

namespace DongleSimulator.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = UserRole.Admin)]
public class AdminController : ControllerBase
{
    [HttpPut("sources/approve/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveSource(
            [FromServices] IApproveSourceUseCase useCase,
            [FromRoute] string id
        )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    
    [HttpPut("sources/deny/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DenySource(
        [FromServices] IDenySourceUseCase useCase,
        [FromRoute] string id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    
    [HttpDelete("sources/delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSource(
        [FromServices] IDeleteSourceByIdUseCase useCase,
        [FromRoute] string id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpPut("templates/approve/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveTemplate(
        [FromServices] IApproveTemplateUseCase useCase,
        [FromRoute] string id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    
    [HttpPut("templates/deny/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DenyTemplate(
        [FromServices] IDenyTemplateUseCase useCase,
        [FromRoute] string id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    
    [HttpDelete("templates/delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTemplate(
        [FromServices] IDeleteTemplateByIdUseCase useCase,
        [FromRoute] string id
    )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    
    [HttpGet("users")]
    [ProducesResponseType(typeof(ResponsesUsersJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers(
        [FromServices] IGetAllUsersUseCase useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var res = await useCase.Execute(page, pageSize);
        return Ok(res);
    }

    [HttpDelete("users/delete/{id}")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(
            [FromServices] IDeleteUserByIdUseCase useCase,
            [FromRoute] string id
        )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    
    [HttpPost("generate-image")]
    [ProducesResponseType(typeof(ResponseGenerateImageJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateImage(
            [FromServices] IGenerateImageUseCase useCase,
            [FromBody] RequestGenerateImageJson? req = null,
            [FromQuery] bool random = true
        )
    {
        var res = await useCase.Execute(req, random);
        return Ok(res);
    }
}