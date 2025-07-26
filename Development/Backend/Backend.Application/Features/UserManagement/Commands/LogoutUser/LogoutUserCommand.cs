using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.LogoutUser
{
    public record LogoutUserCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; } = default!;
    }
}
