using OutScribed.Domain.Models.UserManagement.Entities;

namespace OutScribed.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(Account account);
    }
}
