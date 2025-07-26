using Backend.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.TempUserManagement.Commands.SendRegistrationOTPEmail
{
    [Route(BaseApiPath + "/accounts/send/registration/otp/email")]
    public class AccountController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Sends OTP to email address to create a newly created account",
            Description = "Sends one-time password to a user's email address as a step to create " +
            "a new account. Before a user can " +
            "sign into a newly created account, the email address" +
            "or phone number must be verified. This sends token for the verification."
            )]
        public async Task<ActionResult> SendAccountActivationOTP(
            [FromBody] SendRegistrationOTPEmailRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<SendRegistrationOTPEmailCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
