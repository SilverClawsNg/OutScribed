using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Common
{
    public class ThreadCommentSummary
    {
        public Guid Id { get; set; }

        public Guid ThreadId { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime Date { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public RateTypes? MyRatings { get; set; }

        public string Details { get; set; } = default!;

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int ResponsesCount { get; set; }

        public int Flags { get; set; }

        public bool HasFlagged { get; set; }

        public Guid CommentatorId { get; set; }

        public string CommentatorUsername { get; set; } = default!;

        public string? CommentatorDisplayPhoto { get; set; }

        public List<ThreadCommentSummary>? Responses { get; set; }

    }
}
