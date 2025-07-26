using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.CreateThreadComment
{
    public class CreateThreadCommentCommand : IRequest<Result<CreateThreadCommentResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = default!;

    }
}
