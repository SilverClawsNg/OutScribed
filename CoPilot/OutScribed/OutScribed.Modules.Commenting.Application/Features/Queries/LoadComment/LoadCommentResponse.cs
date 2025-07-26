using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Commenting.Application.Features.Queries.LoadComment
{
    public class LoadCommentResponse
    {
        public Guid Id { get; set; }

        public DateTime CommentedAt { get; set; } = default!;

        public string Details { get; set; } = default!;

        public Guid ContentId { get; set; }

        public string ContentTitle { get; set; } = default!;

        public DateTime ContentDate { get; set; }

        public ContentType ContentType { get; set; }

        public Guid CommentatorId { get; set; }

        public string CommentatorUsername { get; set; } = default!;

        public string? CommentatorPhotoUrl { get; set; }

        public ReactionType MyReaction { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public List<InlineReplies>? Replies { get; set; }

        public class InlineReplies
        {
            public Guid Id { get; set; }

            public DateTime CreatedAt { get; set; } = default!;

            public string Details { get; set; } = default!;

            public Guid ParentId { get; set; }

            public Guid ContentId { get; set; }

            public Guid CreatorId { get; set; }

            public string CreatorUsername { get; set; } = default!;

            public string? CreatorPhotoUrl { get; set; }

            public ReactionType MyReaction { get; set; }

            public int Replies { get; set; }

            public int Upvotes { get; set; }

            public int Downvotes { get; set; }

        }
    }
}
