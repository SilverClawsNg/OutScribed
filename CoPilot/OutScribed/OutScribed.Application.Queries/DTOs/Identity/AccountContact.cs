using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountContact
    {
        public int Id { get; set; }

        public Ulid AccountId { get; set; }

        public ContactType Type { get; set; }

        public string Text { get; set; } = default!;
    }
}
