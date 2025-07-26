using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.SharedKernel.Enums
{
    public enum JailReason
    {
        Unknown = 0,
        TooManyTokenRequests,
        TooManyVerificationAttempts,
        // Add other reasons for jailing here
    }
}
