using OutScribed.Application.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUsernameProfile
{

    [Route(BaseApiPath + "/users/profile/username/{Id}")]

    public class UserController : BaseController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(LoadUsernameProfileQueryResponse), (int)HttpStatusCode.OK)]
        [SwaggerOperation(
            Summary = "Loads user drafts",
            Description = "Loads drafts for a user"
            )]
        public async Task<ActionResult> LoadUsernameProfile(
            [FromRoute] string Id, CancellationToken cancellationToken
            )
        {

            var query = new LoadUsernameProfileQuery(Id, GetAccountId());

            var result = await Mediator.Send(query, cancellationToken);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
