using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.FlagTale
{
    public class FlagTaleRequest
    {
        public Guid TaleId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
