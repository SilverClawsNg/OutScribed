using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;

namespace OutScribed.Modules.Onboarding.Domain.Specifications
{
    public class TempUserByEmailSpecification(string emailAddress) 
        : BaseSpecification<TempUser>(tu => tu.EmailAddress == emailAddress)
    {}
}
