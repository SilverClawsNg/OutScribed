using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Jail.Domain.Models
{
    public class JailedIpAddress : AggregateRoot
    {
        public string IpAddress { get; private set; }
        public JailReason LastJailReason { get; private set; }
        public JailStatus CurrentStatus { get; private set; }
        public DateTime JailedAt { get; private set; }
        public DateTime? ExpiresAt { get; private set; } // Null for permanent bans
        public int TemporaryJailCount { get; private set; } // How many times it has been temporarily jailed
        public DateTime LastUpdated { get; private set; }

        // Private constructor for EF Core/deserialization
        private JailedIpAddress() { }


        // Private constructor for internal use
        private JailedIpAddress(Ulid id, string ipAddress, JailReason reason, JailStatus status, DateTime jailedAt, DateTime? expiresAt, int temporaryJailCount)
        {
            Id = id;
            IpAddress = ipAddress;
            LastJailReason = reason;
            CurrentStatus = status;
            JailedAt = jailedAt;
            ExpiresAt = expiresAt;
            TemporaryJailCount = temporaryJailCount;
            LastUpdated = DateTime.UtcNow;
        }

        // Factory method for creating a new temporary jail record
        public static JailedIpAddress CreateTemporaryJail(Ulid id, string ipAddress, JailReason reason, TimeSpan duration)
        {
            return new JailedIpAddress(id, ipAddress, reason, JailStatus.ActiveTemporary, DateTime.UtcNow, DateTime.UtcNow.Add(duration), 1);
        }

        // --- Domain Methods (Encapsulating logic for JailedIpAddress) ---

        public void ExtendTemporaryJail(JailReason reason, TimeSpan duration)
        {
            if (CurrentStatus != JailStatus.ActivePermanent)
            {

                LastJailReason = reason;
                JailedAt = DateTime.UtcNow; // Reset jail start time
                ExpiresAt = DateTime.UtcNow.Add(duration); // Extend expiry
                TemporaryJailCount++;
                LastUpdated = DateTime.UtcNow;
                CurrentStatus = JailStatus.ActiveTemporary; // Ensure status is temporary
            }

        }

        public void PermanentlyBan(JailReason reason)
        {
            LastJailReason = reason;
            JailedAt = DateTime.UtcNow; // Record permanent ban time
            ExpiresAt = null; // No expiry for permanent ban
            CurrentStatus = JailStatus.ActivePermanent;
            LastUpdated = DateTime.UtcNow;
            // Optionally, you might reset TemporaryJailCount or keep it as a record
        }

        public void UnjailManually()
        {
            CurrentStatus = JailStatus.UnjailedManually;
            ExpiresAt = DateTime.UtcNow; // Mark as expired immediately
            LastUpdated = DateTime.UtcNow;
        }

        public bool IsCurrentlyJailed()
        {
            if (CurrentStatus == JailStatus.ActivePermanent)
            {
                return true;
            }
            if (CurrentStatus == JailStatus.ActiveTemporary && ExpiresAt.HasValue && ExpiresAt.Value > DateTime.UtcNow)
            {
                return true;
            }
            // If it's temporary but expired, or manually unjailed, it's not jailed
            return false;
        }

        public void CheckAndExpire()
        {
            if (CurrentStatus == JailStatus.ActiveTemporary && ExpiresAt.HasValue && ExpiresAt.Value <= DateTime.UtcNow)
            {
                CurrentStatus = JailStatus.Expired;

                LastUpdated = DateTime.UtcNow;
            }
        }
    }
}

