using OutScribed.Application.Web;
using OutScribed.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadSelfTaleDrafts
{

    [Route(BaseApiPath + "/tales/drafts/self")]

    public class TalesController : BaseController
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadSelfTaleDraftsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadSelfTaleDrafts(
            [FromQuery] TaleStatuses? status, [FromQuery] SortTypes? sort, [FromQuery] Categories? category,
            [FromQuery] Countries? country, [FromQuery] string? keyword, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadSelfTaleDraftsQuery(GetAccountId(), status, category, country, sort, keyword, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
