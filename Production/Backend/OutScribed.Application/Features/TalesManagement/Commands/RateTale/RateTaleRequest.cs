using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.RateTale
{
    public class RateTaleRequest
    {
        public Guid TaleId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
