using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.FlagThread
{
    [Route(BaseApiPath + "/threads/flag")]
    public class ThreadController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Rates Tale",
            Description = "Rates a post for sensitive contents"
            )]
        public async Task<ActionResult> FlagThread(
            [FromBody] FlagThreadRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<FlagThreadCommand>(request);

            command.UserId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
