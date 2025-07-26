using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.FlagInsightComment
{
    public class FlagInsightCommentRequest
    {

        public Ulid? InsightId { get; set; }

        public Ulid? CommentId { get; set; }

        public FlagType? Type { get; set; }

    }
}
