using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Commenting.Application.Features.Queries.LoadAllComments
{
    public class LoadAllCommentsResponse
    {
        public Guid ContentId { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<LoadAllComment>? Comments { get; set; }

        public class LoadAllComment
        {
            public Guid Id { get; set; }

            public DateTime CommentedAt { get; set; } = default!;

            public string Details { get; set; } = default!;

            public Guid ContentId { get; set; }

            public Guid CommentatorId { get; set; }

            public string CommentatorUsername { get; set; } = default!;

            public string? CommentatorPhotoUrl { get; set; }

            public ReactionType MyReaction { get; set; }

            public int Replies { get; set; }

            public int Upvotes { get; set; }

            public int Downvotes { get; set; }

        }
    }
}
