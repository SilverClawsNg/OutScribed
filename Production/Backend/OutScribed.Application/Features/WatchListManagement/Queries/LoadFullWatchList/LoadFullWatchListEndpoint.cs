using OutScribed.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.WatchListManagement.Queries.LoadFullWatchList
{

    [Route(BaseApiPath + "/watchlists/{Id}")]

    public class WatchListController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadFullWatchListQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadFullWatchList(
            [FromRoute] Guid Id,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadFullWatchListQuery(Id, GetAccountId());

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
