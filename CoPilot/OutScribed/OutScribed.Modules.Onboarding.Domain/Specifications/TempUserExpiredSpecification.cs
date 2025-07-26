using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;

namespace OutScribed.Modules.Onboarding.Domain.Specifications
{
    public class TempUserExpiredSpecification(DateTime cutoffTime) 
        : BaseSpecification<TempUser>(tu => tu.LastUpdated <= cutoffTime)
    {}
}
