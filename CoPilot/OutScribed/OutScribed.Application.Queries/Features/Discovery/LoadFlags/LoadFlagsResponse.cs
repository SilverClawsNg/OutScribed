using OutScribed.Application.Queries.DTOs.Discovery;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Discovery.LoadFlags
{
    public class LoadFlagsResponse
    {
        public Ulid WatchlistId { get; set; }

        public SortType? Sort { get; set; }

        public FlagType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<WatchlistFlag>? Flags { get; set; }

    }
}
