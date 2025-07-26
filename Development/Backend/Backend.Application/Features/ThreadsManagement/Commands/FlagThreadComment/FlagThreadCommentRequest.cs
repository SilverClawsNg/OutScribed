using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Commands.FlagThreadComment
{
    public class FlagThreadCommentRequest
    {
        public Guid ThreadId { get; set; }

        public Guid CommentId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
