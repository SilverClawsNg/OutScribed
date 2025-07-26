using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.FlagThread
{
    public class FlagThreadRequest
    {
        public Guid ThreadId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
