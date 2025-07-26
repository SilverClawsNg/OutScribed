using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.UserManagement.Commands.AssignRole
{
    [Route(BaseApiPath + "/roles/assign")]
    public class RoleController : BaseController
    {
        [HttpPatch]
        //[Authorize(Roles = "SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Assigns role",
            Description = "Assigns an admin role to a user"
            )]
        public async Task<ActionResult> AssignRole(
            [FromBody] AssignRoleRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<AssignRoleCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();
        }
    }
}
