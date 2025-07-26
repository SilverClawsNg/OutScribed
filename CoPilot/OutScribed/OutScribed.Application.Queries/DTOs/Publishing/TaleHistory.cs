using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Publishing
{

    public class TaleHistory
    {
        public int Id { get; set; }

        public Ulid TaleId { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public string Title { get; set; } = default!;

        public TaleStatus Status { get; set; }

        public string Notes { get; set; } = default!;
    }

}
