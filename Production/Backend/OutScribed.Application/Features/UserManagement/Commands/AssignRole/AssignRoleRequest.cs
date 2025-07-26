using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.UserManagement.Commands.AssignRole
{
    public class AssignRoleRequest
    {

        public RoleTypes? Role { get; set; }

        public Guid AccountId { get; set; }


    }
}
