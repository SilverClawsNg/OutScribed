using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Onboarding.Domain.Interfaces
{
    public interface ITokenProvider
    {
        string GenerateToken();
    }
}
