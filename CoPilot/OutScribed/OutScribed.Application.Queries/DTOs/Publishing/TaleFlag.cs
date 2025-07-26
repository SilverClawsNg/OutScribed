using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Publishing
{
    public class TaleFlag
    {
        public int Id { get; set; }

        public Ulid TaleId { get; set; }

        public Ulid FlagId { get; set; }

        public Ulid AccountId { get; set; }

        public DateTime FlaggedAt { get; set; } = default!;

        public FlagType Type { get; set; }
    }
}
