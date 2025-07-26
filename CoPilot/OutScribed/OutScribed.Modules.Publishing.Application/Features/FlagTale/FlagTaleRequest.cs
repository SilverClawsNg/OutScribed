using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.FlagTale
{
    public class FlagTaleRequest
    {
        public Ulid? TaleId { get; set; }

        public FlagType? Type { get; set; }

    }
}
