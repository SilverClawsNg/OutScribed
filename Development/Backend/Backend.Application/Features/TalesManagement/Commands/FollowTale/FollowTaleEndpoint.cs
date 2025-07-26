using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.TalesManagement.Commands.FollowTale
{
    [Route(BaseApiPath + "/tales/follow")]
    public class TaleController : BaseController
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
        public async Task<ActionResult> FollowTale(
            [FromBody] FollowTaleRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<FollowTaleCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
