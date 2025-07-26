using Asp.Versioning;
using AutoMapper;
using Backend.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Backend.Application.Web
{
    [Route(BaseApiPath)]
    [ApiController]
    [ApiVersion("1.0")]
    public abstract class BaseController : ControllerBase
    {

        protected const string BaseApiPath = "/api";
        //protected const string BaseApiPath = "api/v{version:apiVersion}";
        private IMapper? _mapper;
        private IMediator? _mediator;

        protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected IMapper Mapper 
            => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

        protected ActionResult ErrorOccured(Error error)
        {
            return Problem(
                          title: error.Title,
                          detail: error.Description,
                          statusCode: error.Code,
                          type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6-6-1");
        }

        protected ActionResult NotConfirmed()
        {
            return Problem(
                          title: "Unconfirmed Operation",
                          detail: "This operation was not confirmed. Tick to confirm operation.",
                          statusCode: StatusCodes.Status400BadRequest,
                          type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6-6-1");
        }

        protected Guid GetAccountId()
        {
            var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (accountId == null)
                return Guid.Empty;

            return new Guid(accountId);
        }
    }
}
