using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadReplies
{
    public class LoadRepliesResponse
    {
        public Ulid InsightId { get; set; }

        public Ulid CommentId { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<InsightComment>? Replies { get; set; }

    }
}
