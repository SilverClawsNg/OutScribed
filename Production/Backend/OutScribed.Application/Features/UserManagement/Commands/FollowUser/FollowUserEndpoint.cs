using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.UserManagement.Commands.FollowUser
{
    [Route(BaseApiPath + "/accounts/follow")]
    public class AccountsController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Follows Tale",
            Description = "Follow a post for updates"
            )]
        public async Task<ActionResult> FollowUser(
            [FromBody] FollowUserRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<FollowUserCommand>(request);

            command.FollowerId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
