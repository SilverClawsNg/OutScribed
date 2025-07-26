using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.UserManagement.Commands.RefreshToken
{
    [AllowAnonymous]
    [Route(BaseApiPath + "/accounts/refresh/token")]
    public class AccountController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Renews a token",
            Description = "Verifies a refresh token then issues a new access & refresh token."
            )]
        public async Task<ActionResult> RefreshToken(
            [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<RefreshTokenCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);

        }
    }
}
