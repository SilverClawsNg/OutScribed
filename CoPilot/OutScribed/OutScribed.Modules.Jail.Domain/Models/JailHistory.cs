using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Jail.Domain.Models
{
  
    public class JailHistory : Entity
    {
        public Ulid IpAddressId { get; private set; } 
        public DateTime JailedAt { get; private set; }
        public JailReason Reason { get; private set; }

        // EF Core constructor
        private JailHistory() { }

        private JailHistory(JailReason reason, DateTime jailedAt)
        {
            JailedAt = jailedAt;
            Reason = reason;
        }

        public static JailHistory Record(JailReason reason, DateTime jailedAt)
        {
            return new JailHistory(reason, jailedAt);
        }
    }
}
