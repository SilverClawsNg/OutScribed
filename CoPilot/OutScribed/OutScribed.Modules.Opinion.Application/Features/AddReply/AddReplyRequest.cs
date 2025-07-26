namespace OutScribed.Modules.Analysis.Application.Features.AddReply
{
    public class AddReplyRequest
    {
        public Ulid? InsightId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
