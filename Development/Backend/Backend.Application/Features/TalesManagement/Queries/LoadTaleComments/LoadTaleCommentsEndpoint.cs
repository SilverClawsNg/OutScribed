using Backend.Application.Web;
using Backend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleComments
{

    [Route(BaseApiPath + "/tales/comments/{taleId}")]

    public class TaleController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadTaleCommentsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads post comments",
            Description = "Loads post comments"
            )]
        public async Task<ActionResult> LoadTaleComments(
            [FromRoute] Guid taleId, [FromQuery] string? username, [FromQuery] string? keyword, [FromQuery] SortTypes? sort, 
            [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadTaleCommentsQuery(GetAccountId(), taleId, username, keyword, sort, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
