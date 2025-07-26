using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.CreateThreadResponse
{
    public class CreateThreadResponseCommand : IRequest<Result<CreateThreadResponseResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid CommentatorId { get; set; }

        public Guid ParentId { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = default!;


    }
}
