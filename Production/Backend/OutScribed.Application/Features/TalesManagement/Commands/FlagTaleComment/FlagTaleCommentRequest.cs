using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.FlagTaleComment
{
    public class FlagTaleCommentRequest
    {
        public Guid TaleId { get; set; }

        public Guid CommentId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
