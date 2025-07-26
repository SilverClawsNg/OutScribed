using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleBasic;

[Route(BaseApiPath + "/tales/update/basic")]
public class TaleController : BaseController
{
    [HttpPatch]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a new individual account",
        Description = "Completes the account creation process by adding a new individual user " +
        "and deleting the temporary user account."
        )]
    public async Task<ActionResult> UpdateTaleBasic(
        [FromBody] UpdateTaleBasicRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<UpdateTaleBasicCommand>(request);

        command.AdminId = GetAccountId();

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok();
    }
}
