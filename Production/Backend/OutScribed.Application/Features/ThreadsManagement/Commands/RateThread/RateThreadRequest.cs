using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.RateThread
{
    public class RateThreadRequest
    {
        public Guid ThreadId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
