namespace OutScribed.Modules.Discovery.Application.Features.AddWatchlistReply
{
    public class AddWatchlistReplyResponse()
    {

        public Ulid WatchlistId { get; set; }

        public Ulid CommentId { get; set; }

        public DateTime CommentedAt { get; set; }

        public string Text { get; set; } = default!;
    }
}
