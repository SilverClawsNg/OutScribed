namespace OutScribed.Modules.Analysis.Application.Features.AddAddendum
{
    public class AddAddendumRequest
    {
        public Ulid? InsightId { get; set; }

        public string? Text { get; set; }
    }
}
