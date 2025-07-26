using Backend.Application.Web;
using Backend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleResponses
{

    [Route(BaseApiPath + "/tales/responses/{parentId}")]

    public class TaleController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadTaleResponsesQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads post comments",
            Description = "Loads post comments"
            )]
        public async Task<ActionResult> LoadTaleResponses(
            [FromRoute] Guid parentId, [FromQuery] string? username, [FromQuery] string? keyword, [FromQuery] SortTypes? sort, 
            [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadTaleResponsesQuery(GetAccountId(), parentId, username, keyword, sort, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
