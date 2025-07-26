namespace OutScribed.Modules.Publishing.Application.Features.AddInsightReply
{
    public class AddInsightReplyRequest
    {
        public Ulid? InsightId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
