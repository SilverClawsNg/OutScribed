using Backend.Domain.Models.UserManagement.Entities;

namespace Backend.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(Account account);
    }
}
