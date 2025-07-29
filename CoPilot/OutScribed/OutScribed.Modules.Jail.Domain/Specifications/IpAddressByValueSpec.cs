using OutScribed.Modules.Jail.Domain.Models;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.Modules.Jail.Domain.Specifications
{
  
    public class IpAddressByValueSpec(string value) 
        : BaseSpecification<IpAddress>(jia => jia.Value == value)
    {
    }
}
