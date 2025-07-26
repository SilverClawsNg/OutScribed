using Backend.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.UserManagement.Commands.ResetPassword
{
    [Route(BaseApiPath + "/accounts/password/reset")]
    public class AccountController : BaseController
    {
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Resets a user's password",
            Description = "Resets a user's password after verifying the one-time password" +
            "sent to the user's email address/phone number."
            )]
        public async Task<ActionResult> ResetPassword(
            [FromBody] ResetPasswordRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<ResetPasswordCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
