using OutScribed.Application.Repositories;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using OutScribed.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace OutScribed.Persistence.Repositories
{
    public class TempUserRepository(OutScribedDbContext dbContext)
        : ITempUserRepository
    {
        private readonly OutScribedDbContext _dbContext = dbContext;

        public async Task<TempUser?> GetTempUserByEmailAddress(string emailAddress)
        {
            return await (from user in _dbContext.TempUsers
                          where user.EmailAddress!.Value.ToLower() == emailAddress.ToLower()
                          select user)
                       .SingleOrDefaultAsync();
        }

        public async Task<TempUser?> GetTempUserByPhoneNumber(string phoneNumber)
        {
            return await (from user in _dbContext.TempUsers
                          where user.PhoneNumber!.Value.ToLower() == phoneNumber.ToLower()
                          select user)
                       .SingleOrDefaultAsync();
        }
    }
}
