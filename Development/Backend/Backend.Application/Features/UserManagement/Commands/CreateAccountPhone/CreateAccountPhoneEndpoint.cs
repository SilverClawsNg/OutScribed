using Backend.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend.Application.Features.UserManagement.Commands.CreateAccountPhone;

[Route(BaseApiPath + "/accounts/create/phone")]
public class AccountController : BaseController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a new individual account",
        Description = "Completes the account creation process by adding a new individual user " +
        "and deleting the temporary user account."
        )]
    public async Task<ActionResult> CreateAccountPhone(
        [FromBody] CreateAccountPhoneRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<CreateAccountPhoneCommand>(request);

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok();
    }
}
