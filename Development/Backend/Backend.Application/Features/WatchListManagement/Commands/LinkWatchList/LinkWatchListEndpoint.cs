using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.WatchListManagement.Commands.LinkWatchList
{
    [Route(BaseApiPath + "/watchlists/link")]
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
        public async Task<ActionResult> LinkWatchList(
            [FromBody] LinkWatchListRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<LinkWatchListCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
