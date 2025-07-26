using OutScribed.Application.Web;
using OutScribed.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadWriters
{

    [Route(BaseApiPath + "/users/writers")]

    public class UserController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadWritersQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadWriters(
            [FromQuery] SortTypes? sort, [FromQuery] Countries? country,
            [FromQuery] string? username, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadWritersQuery(GetAccountId(), country, sort, username, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
