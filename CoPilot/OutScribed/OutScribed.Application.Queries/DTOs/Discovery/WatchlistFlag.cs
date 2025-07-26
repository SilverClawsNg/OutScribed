using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Discovery
{
    public class WatchlistFlag
    {
        public int Id { get; set; }

        public Ulid WatchlistId { get; set; }

        public Ulid FlagId { get; set; }

        public Ulid AccountId { get; set; }

        public DateTime FlaggedAt { get; set; } = default!;

        public FlagType Type { get; set; }
    }
}
