using FastEndpoints;

namespace OutScribed.Modules.Onboarding.Application.Features.Commands.VerifyToken
{
  
    public class VerifyTokenEndpoint : Endpoint<VerifyTokenRequest, EmptyResponse>
    {
        public VerifyTokenService Service { get; set; } = default!;

        public override void Configure()
        {
            Post("/api/onboarding/verify-token");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Verifies a verification token.";
                s.Description = "This endpoint initiates the pre-registration process.";
                s.Response(200, "Token verified successfully.");
                s.Response(400, "Bad request due to invalid input or business rule violation.");
                s.Response(429, "Too many requests from this IP address.");
            });
        }

        public override async Task HandleAsync(VerifyTokenRequest req, CancellationToken ct)
        {
            var response = await Service.ExecuteAsync(req, HttpContext);

            //await SendOkAsync(ct); // Or just Ok(ct); if you're not using await for it explicitly

            await SendAsync(response, cancellation: ct);
        }
    }

}
