using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Discovery.Application.Features.FlagWatchlist
{
    public class FlagWatchlistRequest
    {
        public Ulid? WatchlistId { get; set; }

        public FlagType? Type { get; set; }

    }
}
