using Backend.Application.Web;
using Backend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.TalesManagement.Queries.LoadAllTales
{

    [Route(BaseApiPath + "/tales")]

    public class TaleController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadAllTalesQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadAllTales(
            [FromQuery] SortTypes? sort, [FromQuery] Categories? category, [FromQuery] Countries? country,
            [FromQuery] string? username, [FromQuery] Guid? watchlistId, [FromQuery] string? tag, [FromQuery] string? keyword, [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadAllTalesQuery(GetAccountId(), category, country, username, sort, watchlistId, tag, keyword, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
