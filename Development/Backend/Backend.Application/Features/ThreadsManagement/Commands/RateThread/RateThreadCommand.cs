using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.RateThread
{
    public class RateThreadCommand : IRequest<Result<RateThreadResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid UserId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
