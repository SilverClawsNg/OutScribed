using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTaleResponse
{
    [Route(BaseApiPath + "/tales/response")]
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
        public async Task<ActionResult> CreateTaleResponse(
            [FromBody] CreateTaleResponseRequest request, 
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<CreateTaleResponseCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
