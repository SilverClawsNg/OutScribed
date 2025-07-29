using MassTransit;
using OutScribed.Modules.Onboarding.Domain.Events;
using OutScribed.Modules.Onboarding.Domain.Models;
using OutScribed.SharedKernel.Enums;
using OutScribed.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Infrastructure.EventPublishers
{
    public class EventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint; // Inject MassTransit's IPublishEndpoint

        public async Task IpAddressViolated_Jail(string ipAddress, string emailAddress, JailReason reason)
        {
            await _publishEndpoint.Publish(
                new IpAddressViolationEvent(ipAddress, emailAddress, reason)
            );
        }
    }
}
