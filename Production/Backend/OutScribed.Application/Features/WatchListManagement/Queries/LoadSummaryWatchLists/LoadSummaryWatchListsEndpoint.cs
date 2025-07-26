using OutScribed.Application.Web;
using OutScribed.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.WatchListManagement.Queries.LoadSummaryWatchLists
{

    [Route(BaseApiPath + "/watchlists/pool")]

    public class WatchListController : BaseController
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadSummaryWatchListsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadSummaryWatchLists(
            [FromQuery] SortTypes? sort, [FromQuery] Categories? category, [FromQuery] Countries? country,
            [FromQuery] string? keyword, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadSummaryWatchListsQuery(category, country, sort, keyword, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
