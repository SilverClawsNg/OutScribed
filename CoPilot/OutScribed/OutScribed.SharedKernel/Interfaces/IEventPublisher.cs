using OutScribed.SharedKernel.Enums;

namespace OutScribed.SharedKernel.Interfaces
{
    public interface IEventPublisher
    {
        public Task IpAddressViolated_Jail(string ipAddress, string emailAddress, JailReason reason);


    }
}
