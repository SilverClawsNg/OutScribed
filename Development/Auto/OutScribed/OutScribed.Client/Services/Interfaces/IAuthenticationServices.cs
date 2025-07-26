using OutScribed.Client.Enums;

namespace OutScribed.Client.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        Task<bool> CheckJwtTokenAsync();

        Task<RoleTypes> GetAdminRoleAsync();

        Task<bool> CheckLoggedInAsync();

    }
}
