using OutScribed.Application.Queries.DTOs.Identity;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Identity.LoadAdmins
{
    public class LoadAdminsResponse
    {
        public RoleType? Role { get; set; }

        public string? Username { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<AccountAdmin>? Admins { get; set; }

    }
}
