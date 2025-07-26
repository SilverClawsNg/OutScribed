using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.FollowThread
{
    public class FollowThreadCommand : IRequest<Result<FollowThreadResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid AccountId { get; set; }

        public bool Option { get; set; }

    }
}
