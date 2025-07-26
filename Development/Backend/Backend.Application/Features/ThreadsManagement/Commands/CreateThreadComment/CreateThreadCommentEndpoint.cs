using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThreadComment
{
    [Route(BaseApiPath + "/threads/comment")]
    public class ThreadController : BaseController
    {
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Create A Thread Comment",
            Description = "Adds a new comment to a post"
            )]
        public async Task<ActionResult> CreateThreadComment(
            [FromBody] CreateThreadCommentRequest request, 
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<CreateThreadCommentCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
