using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTaleComment
{
    [Route(BaseApiPath + "/tales/comment")]
    public class TaleController : BaseController
    {
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Create A Tale Comment",
            Description = "Adds a new comment to a post"
            )]
        public async Task<ActionResult> CreateTaleComment(
            [FromBody] CreateTaleCommentRequest request, 
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<CreateTaleCommentCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
