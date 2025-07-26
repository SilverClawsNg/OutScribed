using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Onboarding.Application.Interfaces
{
    public interface IOnboardingService
    {
        Task RequestAccessAsync(string email, string ipAddress);
        Task<bool> VerifyAsync(string email, int code);


    }
}
