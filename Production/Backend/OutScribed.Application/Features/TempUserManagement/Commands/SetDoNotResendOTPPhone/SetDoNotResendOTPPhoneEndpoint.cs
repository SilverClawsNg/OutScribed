using OutScribed.Application.Web;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPPhone
{
    [Route(BaseApiPath + "/accounts/stop/registration/otp/phone")]
    public class AccountController : BaseController
    {
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Stops resending OTP",
            Description = "Stops resending otp because of too many requests for 30 minutes."
            )]
        public async Task<ActionResult> SetDoNotResendOTPPhone(
            [FromBody] SetDoNotResendOTPPhoneRequest request,
        CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<SetDoNotResendOTPPhoneCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
