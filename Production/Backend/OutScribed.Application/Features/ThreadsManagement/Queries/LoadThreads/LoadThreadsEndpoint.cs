using OutScribed.Application.Web;
using OutScribed.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreads
{

    [Route(BaseApiPath + "/threads/all")]

    public class ThreadsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadThreadsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadThreads(
            [FromQuery] Guid? threaderId, [FromQuery] SortTypes? sort, [FromQuery] Categories? category,
            [FromQuery] Countries? country, [FromQuery] string? username, [FromQuery] string? tag,
            [FromQuery] string? keyword, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadThreadsQuery(GetAccountId(), threaderId, category, country, username, sort, tag, keyword, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
