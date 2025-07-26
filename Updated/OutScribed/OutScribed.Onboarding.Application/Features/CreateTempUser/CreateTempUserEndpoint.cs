using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using FastEndpoints;
using MediatR;

namespace OutScribed.Onboarding.Application.Features.CreateTempUser;

public class CreateTempUserEndpoint : Endpoint<CreateTempUserCommand>
{
    private readonly IMediator _mediator;

    public CreateTempUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/api/onboarding/request-access");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateTempUserCommand req, CancellationToken ct)
    {
        req = req with { IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown" };
        await _mediator.Send(req, ct);
        await SendAsync(new { message = "Verification code sent." });
    }
}