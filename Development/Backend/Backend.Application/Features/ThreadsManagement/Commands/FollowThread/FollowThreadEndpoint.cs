using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.ThreadsManagement.Commands.FollowThread
{
    [Route(BaseApiPath + "/threads/follow")]
    public class ThreadController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Follows Thread",
            Description = "Follow a post for updates"
            )]
        public async Task<ActionResult> FollowThread(
            [FromBody] FollowThreadRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<FollowThreadCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
