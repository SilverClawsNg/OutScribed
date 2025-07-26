using Backend.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.UserManagement.Commands.UpdateProfile
{
    [Route(BaseApiPath + "/users/update/profile")]
    public class UserController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Updates display photo of a user",
            Description = "A user can update display photo at will."
            )]
        public async Task<ActionResult> UpdatePhoto(
            [FromBody] UpdateProfileRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<UpdateProfileCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok(result.Value);
        }
    }
}
