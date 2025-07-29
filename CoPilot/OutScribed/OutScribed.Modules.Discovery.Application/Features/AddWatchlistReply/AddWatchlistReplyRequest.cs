namespace OutScribed.Modules.Discovery.Application.Features.AddWatchlistReply
{
    public class AddWatchlistReplyRequest
    {
        public Ulid? WatchlistId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
