using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.CreateThreadResponse
{
    [Route(BaseApiPath + "/threads/response")]
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
        public async Task<ActionResult> CreateThreadResponse(
            [FromBody] CreateThreadResponseRequest request, 
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<CreateThreadResponseCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
