using FastEndpoints;

namespace OutScribed.Modules.Onboarding.Application.Features.Commands.SendToken
{

    public class SendTokenEndpoint : Endpoint<SendTokenRequest, SendTokenResponse>
    {
        public SendTokenService Service { get; set; } = default!;

        public override void Configure()
        {
            Post("/api/onboarding/send-token");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Sends a verification token to the provided email address.";
                s.Description = "This endpoint initiates the account verification process.";
                s.Response(200, "Token sent successfully.");
                s.Response(400, "Bad request due to invalid input or business rule violation.");
                s.Response(429, "Too many requests from this IP address.");
            });
        }

        public override async Task HandleAsync(SendTokenRequest req, CancellationToken ct)
        {
            var response = await Service.ExecuteAsync(req, HttpContext);

            //await SendOkAsync(ct); // Or just Ok(ct); if you're not using await for it explicitly

            await SendAsync(response, cancellation: ct);
        }
    }
}
