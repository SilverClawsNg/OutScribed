namespace OutScribed.Modules.Discovery.Application.Features.AddWatchlistComment
{
    public class AddWatchlistCommentRequest
    {
        public Ulid? WatchlistId { get; set; }

        public string? Text { get; set; }
    }
}
