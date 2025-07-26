using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.UserManagement.Common
{
    public class WriterSummary
    {

        public Guid Id { get; set; }

        public string Username { get; set; } = default!;

        public string? RawDisplayPhoto { get; set; }

        public string? DisplayPhoto => IsHidden ? null : RawDisplayPhoto;

        public int Tales { get; set; }

        public int Followers { get; set; }

        public bool IsFollowingUser { get; set; }

        public bool IsHidden { get; set; }

        public Countries Country { get; set; }

        public DateTime WriterDate { get; set; }
    }

}
