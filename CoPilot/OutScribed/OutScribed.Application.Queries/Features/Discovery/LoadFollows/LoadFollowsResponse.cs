using OutScribed.Application.Queries.DTOs.Discovery;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Discovery.LoadFollows
{
    public class LoadFollowsResponse
    {
        public Ulid WatchlistId { get; set; }

        public SortType? Sort { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<WatchlistFollow>? Followers { get; set; }

    }
}
