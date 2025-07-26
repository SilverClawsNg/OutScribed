using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.UserManagement.Commands.LoginUser
{
    [AllowAnonymous]
    [Route(BaseApiPath + "/accounts/login")]
    public class AccountController : BaseController
    {
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Logs in a user",
            Description = "Verifies that a user has a duly registered account and correct login credentials."
            )]
        public async Task<ActionResult> LoginUser(
            [FromBody] LoginUserRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<LoginUserCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);

        }
    }
}
