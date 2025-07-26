using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleCountry;

[Route(BaseApiPath + "/tales/update/country")]
public class TaleController : BaseController
{
    [HttpPatch]
    [Authorize(Roles = "SuperAdmin,Editor,Checker,Publisher,Writer")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
        Summary = "Creates a new individual account",
        Description = "Completes the account creation process by adding a new individual user " +
        "and deleting the temporary user account."
        )]
    public async Task<ActionResult> UpdateTaleCountry(
        [FromBody] UpdateTaleCountryRequest request,
        CancellationToken cancellationToken
        )
    {
        var command = Mapper.Map<UpdateTaleCountryCommand>(request);

        command.AdminId = GetAccountId();

        var result = await Mediator.Send(command, cancellationToken);

        if (result.Error != null)
            return ErrorOccured(result.Error);

        return Ok();
    }
}
