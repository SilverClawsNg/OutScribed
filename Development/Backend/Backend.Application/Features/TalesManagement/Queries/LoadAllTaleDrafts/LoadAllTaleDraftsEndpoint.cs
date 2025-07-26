using Backend.Application.Web;
using Backend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.TalesManagement.Queries.LoadAllTaleDrafts
{

    [Route(BaseApiPath + "/tales/drafts/all")]

    public class TaleController : BaseController
    {
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadAllTaleDraftsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadAllTaleDrafts(
            [FromQuery] TaleStatuses? status, [FromQuery] SortTypes? sort, [FromQuery] Categories? category,
            [FromQuery] Countries? country, [FromQuery] string? username, [FromQuery] string? keyword, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadAllTaleDraftsQuery(status, category, country, username, sort, keyword, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
