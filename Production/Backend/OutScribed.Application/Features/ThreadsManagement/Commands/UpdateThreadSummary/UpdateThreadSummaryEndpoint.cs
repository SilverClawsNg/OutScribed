using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadSummary;

[Route(BaseApiPath + "/threads/update/summary")]
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
    public async Task<ActionResult> UpdateThreadSummary(
        [FromBody] UpdateThreadSummaryRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<UpdateThreadSummaryCommand>(request);

        command.AccountId = GetAccountId();

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok();
    }
}
