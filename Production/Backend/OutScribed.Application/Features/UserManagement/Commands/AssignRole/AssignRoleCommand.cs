using MediatR;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.UserManagement.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<Result<bool>>
    {

        public RoleTypes? Role { get; set; }

        public Guid AccountId { get; set; }

    }

}
