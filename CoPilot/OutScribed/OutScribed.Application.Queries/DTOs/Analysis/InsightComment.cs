using OutScribed.Application.Queries.DTOs.Tagging;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Analysis
{
    public class InsightComment
    {
        public int Id { get; set; }

        public Ulid InsightId { get; set; }

        public Ulid CommentId { get; set; }

        public Ulid? ParentId { get; set; }

        public InsightComment? Parent { get; set; }

        public DateTime CommentedAt { get; set; } = default!;

        public string Text { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public RatingType MyRating { get; set; }

        public int RepliesCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }


        public ICollection<InsightComment> Replies { get; set; } = [];

    }
}
