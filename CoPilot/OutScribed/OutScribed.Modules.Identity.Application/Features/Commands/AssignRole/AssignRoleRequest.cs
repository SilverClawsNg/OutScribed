using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Application.Features.Commands.AssignRole
{
    public class AssignRoleRequest
    {
        public Ulid? AccountId { get; set; }

        public RoleType? Type { get; private set; }

    }
}
