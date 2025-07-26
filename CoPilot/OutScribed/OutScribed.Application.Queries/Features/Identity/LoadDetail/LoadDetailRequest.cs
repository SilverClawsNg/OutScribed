namespace OutScribed.Application.Queries.Features.Identity.LoadDetail
{
    public record LoadAdminsRequest
    {
        public Ulid? AccountId { get; set; }

        public string? Username { get; set; }

    }
}
