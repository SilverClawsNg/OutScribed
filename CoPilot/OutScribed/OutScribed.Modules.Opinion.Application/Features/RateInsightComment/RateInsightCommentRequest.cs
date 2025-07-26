using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.RateInsightComment
{
    public class RateInsightCommentRequest
    {
        public Ulid? InsightId { get; set; }

        public Ulid? CommentId { get; set; }

        public RatingType? Type { get; set; }
    }
}
