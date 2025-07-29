namespace OutScribed.Modules.Discovery.Application.Features.AddWatchlistComment
{
    public class AddInsightCommentResponse()
    {

        public Ulid WatchlistId { get; set; }

        public DateTime CommentedAt { get; set; }

        public string Text { get; set; } = default!;

    }
}
