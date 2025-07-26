namespace OutScribed.Modules.Onboarding.Domain.Exceptions
{
    public class IpAddressSuspendedException : Exception
    {
        public string? IpAddress { get; }

        public IpAddressSuspendedException() : base("IpAddress is currently blocked.") { }

        public IpAddressSuspendedException(string message, Exception innerException) : base(message, innerException) { }

        public IpAddressSuspendedException(string ipAddress)
            : base($"The ipAddress '{ipAddress}' is currently blocked.")
        {
            IpAddress = ipAddress;
        }

        public IpAddressSuspendedException(string ipAddress, string message)
            : base(message)
        {
            IpAddress = ipAddress;
        }
    }
}
