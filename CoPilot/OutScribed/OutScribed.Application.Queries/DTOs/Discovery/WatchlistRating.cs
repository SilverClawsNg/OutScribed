using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Discovery
{
    public class WatchlistRating
    {
        public int Id { get; set; }

        public Ulid WatchlistId { get; set; }

        public Ulid RatingId { get; set; }

        public Ulid AccountId { get; set; }

        public DateTime RatedAt { get; set; } = default!;

        public RatingType Type { get; set; }

    }
}
