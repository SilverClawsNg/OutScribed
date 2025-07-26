using OutScribed.Modules.Onboarding.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Specifications
{
   
    public class DistinctEmailsForIpSpecification : Specification<TempUser, string>
    {
        public DistinctEmailsForIpSpecification(string ipAddress, TimeSpan timeWindow)
        {
            Query
                .Where(tu => tu.IpAddress == ipAddress && tu.LastUpdated >= DateTime.UtcNow.Subtract(timeWindow))
                .Select(tu => tu.EmailAddress) // Select only the email addresses
                .Distinct(); // Get distinct email addresses
        }
    }
}
