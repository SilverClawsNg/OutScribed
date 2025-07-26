using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Infrastructure.EmailServices
{
    public interface IEmailService
    {
        Task SendTempUserTokenEmailAsync(string emailAddress, string token);

        Task ResendTempUserTokenEmailAsync(string emailAddress, string token);

    }
}
