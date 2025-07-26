using Backend.Application.Web;
using Backend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.UserManagement.Queries.LoadAdmins
{

    [Route(BaseApiPath + "/users/admins")]

    public class UserController : BaseController
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadAdminsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadAdmins(
            [FromQuery] RoleTypes? role, [FromQuery] SortTypes? sort, [FromQuery] Countries? country,
            [FromQuery] string? username, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadAdminsQuery(role, country, sort, username, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
