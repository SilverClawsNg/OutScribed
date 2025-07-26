using OutScribed.Onboarding.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Onboarding.Domain.Interfaces
{
    public interface ITempUserRepository
    {

        Task<TempUser?> GetByEmailAsync(string email);
        Task CreateAsync(TempUser user);
        Task UpdateAsync(TempUser user);


        Task<int> CountRequestsFromIpAsync(string ipAddress, TimeSpan within);
        Task<int> CountRequestsFromUserAsync(string email, TimeSpan within);
        //Task<int> CountRequestsFromIpAsync(string ipAddress, TimeSpan within);

        //Task<int> CountRequestsFromUserAsync(string email, TimeSpan within);
    }
}
