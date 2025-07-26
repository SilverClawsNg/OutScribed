using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.WatchListManagement.Commands.FollowWatchList
{
    [Route(BaseApiPath + "/watchlists/follow")]
    public class WatchListController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Follows WatchList",
            Description = "Follow a post for updates"
            )]
        public async Task<ActionResult> FollowWatchList(
            [FromBody] FollowWatchListRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<FollowWatchListCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
