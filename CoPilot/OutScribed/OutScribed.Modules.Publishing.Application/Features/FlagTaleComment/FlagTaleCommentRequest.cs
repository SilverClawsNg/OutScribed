using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.FlagTaleComment
{
    public class FlagTaleCommentRequest
    {

        public Ulid? TaleId { get; set; }

        public Ulid? CommentId { get; set; }

        public FlagType? Type { get; set; }

    }
}
