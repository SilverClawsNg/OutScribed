using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateRole
{
    public class UpdateRoleRequest
    {
        public RoleType? Type { get; set; }

        public bool? IsActive { get; set; }

        public Ulid? AccountId { get; set; }
    }
}
