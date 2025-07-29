using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.Infrastructure.EmailServices
{
    public class EmailService : IEmailService
    {
        public Task ResendTempUserTokenEmailAsync(string emailAddress, string token)
        {
            throw new NotImplementedException();
        }

        public Task SendTempUserTokenEmailAsync(string emailAddress, string token)
        {
            throw new NotImplementedException();
        }

    }
}
