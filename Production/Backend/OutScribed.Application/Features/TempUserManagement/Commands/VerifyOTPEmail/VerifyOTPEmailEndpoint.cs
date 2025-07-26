using OutScribed.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TempUserManagement.Commands.VerifyOTPEmail;


[Route(BaseApiPath + "/accounts/verify/registration/otp/email")]
public class AccountController : BaseController
{
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Verifies OTP to email address to create a newly created account",
        Description = "Verifies one-time password sent to a user's email address as a step to create " +
        "a new account. Before a user can " +
        "sign into a newly created account, the email address" +
        "or phone number must be verified. This verifies the sent token."
        )]
    public async Task<ActionResult> VerifyTempUserOTP(
        [FromBody] VerifyOTPEmailRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<VerifyOTPEmailCommand>(request);

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok();
    }
}
