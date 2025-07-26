using Backend.Domain.Enums;

namespace Backend.Application.Features.TalesManagement.Commands.RateTaleComment
{
    public class RateTaleCommentRequest
    {
        public Guid TaleId { get; set; }

        public Guid CommentId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
