using OutScribed.SharedKernel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Application.Repository
{
    public interface ITempUserRepository
    {
        // Method for the specific query that needed projection and distinct count
        Task<int> CountDistinctEmailsForIpInTimeWindowAsync(string ipAddress, TimeSpan timeWindow);

        // Add any other read-specific methods here.
        // For example, if you later need to display a list of pending pre-registrations:
        // Task<List<PreRegistrationSummaryDto>> GetPendingPreRegistrationsAsync();
        // (Where PreRegistrationSummaryDto is a DTO with only necessary fields)
    }
}
