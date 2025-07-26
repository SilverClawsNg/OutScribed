namespace OutScribed.Modules.Discovery.Application.Features.AddReply
{
    public class AddReplyRequest
    {
        public Ulid? WatchlistId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
