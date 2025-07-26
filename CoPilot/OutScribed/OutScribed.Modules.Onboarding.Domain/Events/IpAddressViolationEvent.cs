using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Onboarding.Domain.Events
{
  
    public class IpAddressViolationEvent(string ipAddress, string emailAddress, JailReason reason) 
        : DomainEvent
    {
        public string IpAddress { get; } = ipAddress;
        public string EmailAddress { get; } = emailAddress;
        public JailReason Reason { get; } = reason;
    }
}
