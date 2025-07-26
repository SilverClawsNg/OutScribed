using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.RateTaleComment
{
    public class RateTaleCommentRequest
    {
        public Ulid? TaleId { get; set; }

        public Ulid? CommentId { get; set; }

        public RatingType? Type { get; set; }
    }
}
