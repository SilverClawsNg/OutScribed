using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadPhoto;

[Route(BaseApiPath + "/threads/update/photo")]
public class ThreadController : BaseController
{
    [HttpPatch]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a new individual account",
        Description = "Completes the account creation process by adding a new individual user " +
        "and deleting the temporary user account."
        )]
    public async Task<ActionResult> UpdateThreadPhoto(
        [FromBody] UpdateThreadPhotoRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<UpdateThreadPhotoCommand>(request);

        command.AccountId = GetAccountId();

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok(result.Value);
    }
}
