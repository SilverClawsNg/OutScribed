using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Common
{
    public class AdminSummary
    {

        public Guid Id { get; set; }

        public RoleTypes Role { get; set; }

        public Countries Country { get; set; }

        public string Address { get; set; } = default!;

        public string Username { get; set; } = default!;

        public string ApplicationUrl { get; set; } = default!;

        public DateTime ApplicationDate { get; set; }

    }
}
