using MediatR;
using Backend.Domain.Exceptions;
using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.AssignRole
{
    public class AssignRoleCommand : IRequest<Result<bool>>
    {

        public RoleTypes? Role { get; set; }

        public Guid AccountId { get; set; }

    }

}
