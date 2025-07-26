namespace OutScribed.Modules.Discovery.Application.Features.AddComment
{
    public class AddCommentResponse()
    {

        public Ulid WatchlistId { get; set; }

        public DateTime CommentedAt { get; set; }

        public string Text { get; set; } = default!;

    }
}
