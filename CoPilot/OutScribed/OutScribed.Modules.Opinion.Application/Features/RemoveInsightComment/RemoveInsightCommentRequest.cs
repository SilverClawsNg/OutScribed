namespace OutScribed.Modules.Analysis.Application.Features.RemoveInsightComment
{
    public class RemoveInsightCommentRequest
    {
        public Ulid? InsightId { get; set; }

        public Ulid? CommentId { get; set; }

    }
}
