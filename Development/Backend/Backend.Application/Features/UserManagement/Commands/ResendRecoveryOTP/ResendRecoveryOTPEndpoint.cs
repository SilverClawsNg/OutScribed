using Backend.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.UserManagement.Commands.ResendRecoveryOTP
{
    [Route(BaseApiPath + "/accounts/resend/recovery/otp")]
    public class AccountController : BaseController
    {
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Resends OTP for registration",
            Description = "Resends one-time password to a user's email address or phone number " +
            "for registration verification step."
            )]
        public async Task<ActionResult> SendAccountActivationOTP(
            [FromBody] ResendRecoveryOTPRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<ResendRecoveryOTPCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
