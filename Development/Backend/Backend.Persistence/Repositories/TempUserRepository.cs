using Backend.Application.Repositories;
using Backend.Domain.Models.TempUserManagement.Entities;
using Backend.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.Repositories
{
    public class TempUserRepository(BackendDbContext dbContext) 
        : ITempUserRepository
    {
        private readonly BackendDbContext _dbContext = dbContext;

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
