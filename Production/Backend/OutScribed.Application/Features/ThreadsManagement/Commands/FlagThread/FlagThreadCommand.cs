using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.FlagThread
{
    public class FlagThreadCommand : IRequest<Result<FlagThreadResponse>>
    {
        public Guid ThreadId { get; set; }

        public Guid UserId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
