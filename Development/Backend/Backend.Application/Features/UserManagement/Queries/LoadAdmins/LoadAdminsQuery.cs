using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.UserManagement.Queries.LoadAdmins
{
    public record LoadAdminsQuery(RoleTypes? Role, Countries? Country, 
        SortTypes? Sort, string? Username, int? Pointer, int? Size)
        : IRequest<LoadAdminsQueryResponse>
    {

        public RoleTypes? Role { get; set; } = Role;

        public Countries? Country { get; set; } = Country;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Username { get; set; } = Username;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
