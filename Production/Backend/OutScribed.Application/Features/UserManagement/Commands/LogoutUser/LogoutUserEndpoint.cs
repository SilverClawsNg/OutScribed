using OutScribed.Application.Web;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.UserManagement.Commands.LogoutUser
{
    [Route(BaseApiPath + "/accounts/logout")]
    public class AccountController : BaseController
    {
        [HttpPatch]
        [SwaggerOperation(
            Summary = "Logs out user",
            Description = "Deletes refresh token from a user's account"
            )]
        public async Task LogoutUser(
            [FromBody] LogoutUserRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = Mapper.Map<LogoutUserCommand>(request);

            await Mediator.Send(command, cancellationToken);

        }
    }
}
