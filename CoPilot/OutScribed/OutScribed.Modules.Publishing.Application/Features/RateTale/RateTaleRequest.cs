using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.RateTale
{
    public class RateTaleRequest
    {
        public Ulid? TaleId { get; set; }

        public RatingType? Type { get; set; }
    }
}
