using Backend.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.ThreadsManagement.Queries.LoadThreadComment
{

    [Route(BaseApiPath + "/threads/comment/{Id}")]

    public class ThreadController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadThreadCommentQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads post comments",
            Description = "Loads post comments"
            )]
        public async Task<ActionResult> LoadThreadComment(
            [FromRoute] Guid Id, CancellationToken cancellationToken)
        {

            var query = new LoadThreadCommentQuery(GetAccountId(), Id);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
