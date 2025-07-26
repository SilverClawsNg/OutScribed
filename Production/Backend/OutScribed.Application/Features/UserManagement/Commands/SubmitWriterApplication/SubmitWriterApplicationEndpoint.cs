using OutScribed.Application.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace OutScribed.Application.Features.UserManagement.Commands.SubmitWriterApplication
{
    [Route(BaseApiPath + "/users/submit/application")]
    public class UserController : BaseController
    {
        [HttpPatch]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Updates display photo of a user",
            Description = "A user can update display photo at will."
            )]
        public async Task<ActionResult> UpdatePhoto(
            [FromBody] SubmitWriterApplicationRequest request,
            CancellationToken cancellationToken
            )
        {

            var command = Mapper.Map<SubmitWriterApplicationCommand>(request);

            command.AccountId = GetAccountId();

            var result = await Mediator.Send(command, cancellationToken);

            if (result.Error != null)
                return ErrorOccured(result.Error);

            return Ok();

        }
    }
}
