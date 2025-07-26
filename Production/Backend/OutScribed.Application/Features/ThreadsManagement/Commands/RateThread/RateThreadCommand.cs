using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.RateThread
{
    public class RateThreadCommand : IRequest<Result<RateThreadResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid UserId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
