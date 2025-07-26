using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.TalesManagement.Commands.RateTale
{
    [Route(BaseApiPath + "/tales/rate")]
    public class TaleController : BaseController
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
        public async Task<ActionResult> RateTale(
            [FromBody] RateTaleRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<RateTaleCommand>(request);

            command.UserId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
