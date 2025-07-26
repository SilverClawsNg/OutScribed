using Backend.Application.Features.UserManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.LoginUser
{

    public class LoginUserResponse
    {

        public bool IsSuccessful { get; set; }

        public string Token { get; set; } = default!;

        public string RefreshToken { get; set; } = default!;

        public string Username { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? Bio { get; set; }

        public string? DisplayPhoto { get; set; }

        public RoleTypes Role { get; set; }

        public int Followers { get; set; }

        public int ProfileViews { get; set; }

        public List<Contacts> Contacts { get; set; } = default!;

        public DateTime RegisterDate { get; set; }

        public bool IsHidden { get; set; }

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }
    }

}
