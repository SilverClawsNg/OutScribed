using OutScribed.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUserProfile
{

    [Route(BaseApiPath + "/users/profile/{Id}")]

    public class UserController : BaseController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadUserProfileQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadUserProfile(
            [FromRoute] Guid Id, CancellationToken cancellationToken
            )
        {

            var query = new LoadUserProfileQuery(Id, GetAccountId());

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
