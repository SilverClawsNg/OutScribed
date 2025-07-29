using OutScribed.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Jail.Application.Interfaces
{
    public interface IJailService
    {
        
        Task ProcessViolationAsync(string ipAddress, string emailAddress, JailReason reason);
        
        Task<bool> IsCurrentlyJailedAsync(string ipAddress);

        //Manually releases an Ip Address from jail
        Task ReleaseIpAddress(string ipAddressValue);


    }
}
