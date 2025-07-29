namespace OutScribed.Modules.Discovery.Application.Features.RemoveWatchlistComment
{
    public class RemoveWatchlistCommentRequest
    {
        public Ulid? WatchlistId { get; set; }

        public Ulid? CommentId { get; set; }

    }
}
