using OutScribed.Domain.Enums;
using MediatR;
using OutScribed.Domain.Exceptions;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.RateThreadComment
{
    public class RateThreadCommentCommand : IRequest<Result<RateThreadCommentResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CommentId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
