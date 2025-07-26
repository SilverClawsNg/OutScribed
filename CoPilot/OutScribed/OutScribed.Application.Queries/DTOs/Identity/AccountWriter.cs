using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountWriter
    {
        public int Id { get; set; }

        public Ulid AccountId { get; set; } = default!;

        public string? Photo { get; set; }

        public int TalesCount { get; set; }

        public int FollowersCount { get; set; }

        public bool IsFollowingUser { get; set; }

        public bool IsHidden { get; set; }

        public Country Country { get; set; }

        public DateTime ApprovedAt { get; set; }
    }
}
