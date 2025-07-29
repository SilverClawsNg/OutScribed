namespace OutScribed.Modules.Analysis.Application.Features.AddInsightAddendum
{
    public class AddInsightAddendumRequest
    {
        public Ulid? InsightId { get; set; }

        public string? Text { get; set; }
    }
}
