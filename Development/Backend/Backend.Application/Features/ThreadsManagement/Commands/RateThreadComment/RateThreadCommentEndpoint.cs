using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.ThreadsManagement.Commands.RateThreadComment
{
    [Route(BaseApiPath + "/threads/rate/comment")]
    public class ThreadController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Rates Thread",
            Description = "Rates a post for sensitive contents"
            )]
        public async Task<ActionResult> RateThreadComment(
            [FromBody] RateThreadCommentRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<RateThreadCommentCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
