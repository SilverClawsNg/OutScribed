using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Discovery.Application.Features.RateWatchlist
{
    public class RateWatchlistRequest
    {
        public Ulid? WatchlistId { get; set; }

        public RatingType? Type { get; set; }
    }
}
