using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;

namespace OutScribed.Modules.Onboarding.Domain.Specifications
{
    public class TempUserByIdSpecification(Ulid id) 
        : BaseSpecification<TempUser>(tu => tu.Id == id)
    {}
}
