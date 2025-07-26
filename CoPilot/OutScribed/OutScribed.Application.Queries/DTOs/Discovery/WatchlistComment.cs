using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Discovery
{
    public class WatchlistComment
    {
        public int Id { get; set; }

        public Ulid WatchlistId { get; set; }

        public Ulid CommentId { get; set; }

        public Ulid? ParentId { get; set; }

        public WatchlistComment? Parent { get; set; }

        public DateTime CommentedAt { get; set; } = default!;

        public string Text { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public RatingType MyRating { get; set; }

        public int RepliesCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }

        public ICollection<WatchlistComment> Replies { get; set; } = [];

    }
}
