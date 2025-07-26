using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.WatchListManagement.Commands.UpdateWatchList;

[Route(BaseApiPath + "/watchlists/update")]
public class WatchListController : BaseController
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a new individual account",
        Description = "Completes the account creation process by adding a new individual user " +
        "and deleting the temporary user account."
        )]
    public async Task<ActionResult> UpdateWatchList(
        [FromBody] UpdateWatchListRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<UpdateWatchListCommand>(request);

                var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok(result.Value);
    }
}
