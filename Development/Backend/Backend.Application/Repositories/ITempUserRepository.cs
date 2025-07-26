using Backend.Domain.Models.TempUserManagement.Entities;

namespace Backend.Application.Repositories
{
    public interface ITempUserRepository
    {
        /// <summary>
        /// Gets a temp user by email address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        Task<TempUser?> GetTempUserByEmailAddress(string emailAddress);

        /// <summary>
        /// Gets a temp user by telephone number
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        Task<TempUser?> GetTempUserByPhoneNumber(string phoneNumber);
    }
}
