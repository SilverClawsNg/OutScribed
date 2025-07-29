using Hangfire;
using Microsoft.Extensions.Logging;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.Infrastructure.BackgroundJobs.EnqueuedJobs
{
    public class EmailJobs(IBackgroundJobClient hangfireClient, ILogger logger) 
        : IEmailEnqueuer
    {
        private readonly IBackgroundJobClient _hangfireClient = hangfireClient ?? throw new ArgumentNullException(nameof(hangfireClient));
        private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public void EnqueueTempUserResendTokenEmail(string emailAddress, string token)
        {
            _hangfireClient.Enqueue<IEmailService>(service => service.ResendTempUserTokenEmailAsync(emailAddress, token));
        }

        public void EnqueueTempUserSendTokenEmail(string emailAddress, string token)
        {
            //_logger.Information("Scheduling verification email for TempUser {TempUserId} to {EmailAddress}", tempUserId, emailAddress);
            _hangfireClient.Enqueue<IEmailService>(service => service.SendTempUserTokenEmailAsync(emailAddress, token));
        }

    }
}
