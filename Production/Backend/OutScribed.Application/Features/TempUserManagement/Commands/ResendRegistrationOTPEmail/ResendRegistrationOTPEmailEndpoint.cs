using OutScribed.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TempUserManagement.Commands.ResendRegistrationOTPEmail
{
    [Route(BaseApiPath + "/accounts/resend/registration/otp/email")]
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
            [FromBody] ResendRegistrationOTPEmailRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<ResendRegistrationOTPEmailCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
