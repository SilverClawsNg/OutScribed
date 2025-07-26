using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.ShareTale
{
    public class ShareTaleRequest
    {
        public Ulid? TaleId { get; set; }

        public string? Handle { get; set; }

        public ContactType? Contact { get; set; }
    }
}
