namespace OutScribed.Modules.Analysis.Application.Features.AddInsightComment
{
    public class AddInsightCommentRequest
    {
        public Ulid? InsightId { get; set; }

        public string? Text { get; set; }
    }
}
