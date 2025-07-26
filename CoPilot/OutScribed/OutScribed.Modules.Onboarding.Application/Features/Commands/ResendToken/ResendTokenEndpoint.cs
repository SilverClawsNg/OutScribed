using FastEndpoints;

namespace OutScribed.Modules.Onboarding.Application.Features.Commands.ResendToken
{
    public class ResendTokenEndpoint : Endpoint<ResendTokenRequest, EmptyResponse>
    {
        public ResendTokenService Service { get; set; } = default!;

        public override void Configure()
        {
            Post("/api/onboarding/resend-token");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Resends a verification token to the provided email address.";
                s.Description = "This endpoint initiates the account verification process.";
                s.Response(200, "Token sent successfully.");
                s.Response(400, "Bad request due to invalid input or business rule violation.");
                s.Response(429, "Too many requests from this IP address.");
            });
        }

        public override async Task HandleAsync(ResendTokenRequest req, CancellationToken ct)
        {
            await Service.ExecuteAsync(req, HttpContext);
            await SendOkAsync(ct);
        }
    }
}
