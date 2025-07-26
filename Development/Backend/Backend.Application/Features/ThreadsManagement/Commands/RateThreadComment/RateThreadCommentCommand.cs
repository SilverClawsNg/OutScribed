using Backend.Domain.Enums;
using MediatR;
using Backend.Domain.Exceptions;

namespace Backend.Application.Features.ThreadsManagement.Commands.RateThreadComment
{
    public class RateThreadCommentCommand : IRequest<Result<RateThreadCommentResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CommentId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
