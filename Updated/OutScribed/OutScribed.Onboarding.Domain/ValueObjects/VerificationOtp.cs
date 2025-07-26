using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutScribed.SharedKernel.BaseTypes;

namespace OutScribed.Onboarding.Domain.ValueObjects
{
    public class VerificationOtp : ValueObject
    {
        public int Code { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool IsExpired => DateTime.UtcNow > CreatedAt.AddMinutes(10);

        private VerificationOtp() { } // For EF

        public static VerificationOtp Generate()
        {
            var random = new Random();
            var code = random.Next(100000, 999999); // 6-digit OTP
            return new VerificationOtp { Code = code, CreatedAt = DateTime.UtcNow };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
            yield return CreatedAt;
        }
    }
}