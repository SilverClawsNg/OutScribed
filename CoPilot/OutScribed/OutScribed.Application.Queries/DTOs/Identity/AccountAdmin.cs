using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountAdmin
    {
        public int Id { get; set; }

        public Ulid AdminId { get; set; }

        public Ulid AccountId { get; set; }

        public RoleType Role { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
