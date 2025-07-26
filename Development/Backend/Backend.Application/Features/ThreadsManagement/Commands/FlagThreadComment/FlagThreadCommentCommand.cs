using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.FlagThreadComment
{
    public class FlagThreadCommentCommand : IRequest<Result<FlagThreadCommentResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
