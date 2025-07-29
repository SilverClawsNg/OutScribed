namespace OutScribed.Modules.Publishing.Application.Features.AddTaleReply
{
    public class AddTaleReplyRequest
    {
        public Ulid? InsightId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
