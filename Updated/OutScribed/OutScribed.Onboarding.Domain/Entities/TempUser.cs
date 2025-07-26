using OutScribed.SharedKernel.BaseTypes;
using OutScribed.SharedKernel.Interfaces;
using OutScribed.Onboarding.Domain.ValueObjects;

namespace OutScribed.Onboarding.Domain.Entities
{
    public class TempUser : Entity, IAggregateRoot
    {
        public DateTime RequestedAt { get; private set; }
        public string Email { get; private set; }
        public VerificationOtp Otp { get; private set; }
        public bool IsVerified { get; private set; }
        public bool LockResends { get; private set; }
        public int ResendsCounter { get; private set; }
        public string IpAddress { get; private set; }

        public DateTime LastOtpSentAt { get; private set; }

        protected TempUser() { } // For EF Core

        public TempUser(string email, string ipAddress)
        {
            Id = Guid.NewGuid();
            RequestedAt = DateTime.UtcNow;
            Email = email;
            IpAddress = ipAddress;
            Otp = VerificationOtp.Generate();
            IsVerified = false;
            LockResends = false;
            ResendsCounter = 0;
        }

        public void Verify(int code)
        {
            if (Otp.IsExpired)
                throw new InvalidOperationException("OTP expired.");

            if (Otp.Code != code)
                throw new UnauthorizedAccessException("Invalid OTP code.");

            IsVerified = true;
        }

        public void ResendOtp()
        {
            if (LockResends)
                throw new InvalidOperationException("Resends locked.");

            if (DateTime.UtcNow < LastOtpSentAt.AddSeconds(90))
                throw new InvalidOperationException("Please wait before requesting a new OTP.");

            Otp = VerificationOtp.Generate();
            LastOtpSentAt = DateTime.UtcNow;
            ResendsCounter++;

            if (ResendsCounter > 3)
                LockResends = true;
        }
    }
}