using OutScribed.Modules.Jail.Domain.Models;
using OutScribed.SharedKernel.Abstract.OutScribed.SharedKernel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Jail.Domain.Specifications
{
  
    public class JailedIpAddressByIpSpecification : BaseSpecification<JailedIpAddress>
    {
        public JailedIpAddressByIpSpecification(string ipAddress)
            : base(jia => jia.IpAddress == ipAddress)
        {
        }
    }
}
