using MassTransit;
using OutScribed.Modules.Jail.Application.Interfaces;
using OutScribed.Modules.Onboarding.Domain.Events;

namespace OutScribed.Infrastructure.EventConsumers
{
    public class IpAddressViolationEventConsumer(IJailService jailService) 
        : IConsumer<IpAddressViolationEvent>
    {
        private readonly IJailService _jailService = jailService; // Inject the Jail Module's service

        public async Task Consume(ConsumeContext<IpAddressViolationEvent> context)
        {
            var message = context.Message;
            await _jailService.ProcessViolationAsync(message.IpAddress, message.EmailAddress, message.Reason);
            // This service will contain the logic to update or create JailedIpAddress
        }
    }


}
