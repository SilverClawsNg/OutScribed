using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.AssignRole
{
    public class AssignRoleRequest
    {

        public RoleTypes? Role { get; set; }

        public Guid AccountId { get; set; }


    }
}
