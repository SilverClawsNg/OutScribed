using Backend.Application.Web;
using Backend.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleComment
{

    [Route(BaseApiPath + "/tales/comment/{Id}")]

    public class TaleController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadTaleCommentQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads post comments",
            Description = "Loads post comments"
            )]
        public async Task<ActionResult> LoadTaleComment(
            [FromRoute] Guid Id, CancellationToken cancellationToken)
        {

            var query = new LoadTaleCommentQuery(GetAccountId(), Id);

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
