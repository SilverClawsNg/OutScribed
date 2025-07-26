namespace OutScribed.Modules.Discovery.Application.Features.AddComment
{
    public class AddCommentRequest
    {
        public Ulid? WatchlistId { get; set; }

        public string? Text { get; set; }
    }
}
