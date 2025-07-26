using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.PublishThread;

[Route(BaseApiPath + "/threads/update/status")]
public class ThreadController : BaseController
{
    [HttpPatch]
    [Authorize(Roles = "SuperAdmin,Editor,Checker,Publisher,Writer")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a new individual account",
        Description = "Completes the account creation process by adding a new individual user " +
        "and deleting the temporary user account."
        )]
    public async Task<ActionResult> PublishThread(
        [FromBody] PublishThreadRequest request,
        CancellationToken cancellationToken
        )
    {

        if (request.Confirm == false)
            return NotConfirmed();

        var command = Mapper.Map<PublishThreadCommand>(request);

        command.AccountId = GetAccountId();

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok();
    }
}
