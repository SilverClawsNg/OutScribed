namespace OutScribed.Modules.Discovery.Application.Features.UpdateWatchlistComment
{
    public class UpdateWatchlistCommentRequest
    {
        public Ulid? WatchlistId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
