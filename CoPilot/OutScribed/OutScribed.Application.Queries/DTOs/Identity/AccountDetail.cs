using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountDetail
    {
        public int Id { get; set; }

        public Ulid AccountId { get; set; }

        public string Username { get; set; } = default!;

        public string Photo { get; set; } = default!;

        public string Bio { get; set; } = default!;

        public string Title { get; set; } = default!;

        public DateTime RegisteredAt { get; set; }

        public int FollowersCount { get; set; }

        public bool IsFollowingUser { get; set; }

        public bool IsHidden { get; set; }

        public Country Country { get; set; }

        public ICollection<AccountContact> Contacts { get; set; } = [];

    }
}
