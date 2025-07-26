using Backend.Application.Features.UserManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Queries.LoadAdmins
{

    public class LoadAdminsQueryResponse
    {

        public RoleTypes? Role { get; set; }

        public Countries? Country { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Username { get; set; }

        public List<AdminSummary>? Admins { get; set; }

    }

}