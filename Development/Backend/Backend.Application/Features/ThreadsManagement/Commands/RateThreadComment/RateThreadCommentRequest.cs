using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Commands.RateThreadComment
{
    public class RateThreadCommentRequest
    {
        public Guid ThreadId { get; set; }

        public Guid CommentId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
