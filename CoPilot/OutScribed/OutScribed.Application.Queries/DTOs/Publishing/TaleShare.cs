using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Publishing
{
    public class TaleShare
    {
        public int Id { get; set; }

        public Ulid TaleId { get; set; }

        public Ulid ShareId { get; set; }

        public Ulid? UserId { get; set; }

        public DateTime SharedAt { get; set; } = default!;

        public ContactType Type { get; set; }

        public string Handle { get; set; } = default!;

    }
}
