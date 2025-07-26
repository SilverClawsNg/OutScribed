using OutScribed.Application.Web;
using OutScribed.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadComments
{

    [Route(BaseApiPath + "/threads/comments/{taleId}")]

    public class ThreadController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadThreadCommentsQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads post comments",
            Description = "Loads post comments"
            )]
        public async Task<ActionResult> LoadThreadComments(
            [FromRoute] Guid taleId, [FromQuery] string? username, [FromQuery] string? keyword, [FromQuery] SortTypes? sort,
            [FromQuery] int? pointer, [FromQuery] int? size,
            CancellationToken cancellationToken
            )
        {

            var query = new LoadThreadCommentsQuery(GetAccountId(), taleId, username, keyword, sort, pointer, size);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
