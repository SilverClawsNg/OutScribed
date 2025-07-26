using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Analysis
{
    public class InsightFlag
    {
        public int Id { get; set; }

        public Ulid InsightId { get; set; }

        public Ulid FlagId { get; set; }

        public Ulid AccountId { get; set; }

        public DateTime FlaggedAt { get; set; } = default!;

        public FlagType Type { get; set; }
    }
}
