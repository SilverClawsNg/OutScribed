using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Discovery.LoadRatings
{
    public class LoadRatingsRequest
    {
        public Ulid WatchlistId { get; set; }

        public RatingType? Type { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
