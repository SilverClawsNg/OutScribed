namespace OutScribed.Modules.Analysis.Application.Features.AddComment
{
    public class AddCommentRequest
    {
        public Ulid? InsightId { get; set; }

        public string? Text { get; set; }
    }
}
