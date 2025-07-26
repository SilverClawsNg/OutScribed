using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.UserManagement.Commands.UpdatePassword
{
    [Route(BaseApiPath + "/accounts/update/password")]
    public class AccountController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Updates a user's password",
            Description = "Updates a user's password on the request of the user."
            )]
        public async Task<ActionResult> UpdatePassword(
            [FromBody] UpdatePasswordRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<UpdatePasswordCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
