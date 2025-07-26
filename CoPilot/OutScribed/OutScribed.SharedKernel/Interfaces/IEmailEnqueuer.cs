namespace OutScribed.SharedKernel.Interfaces
{
    public interface IEmailEnqueuer
    {
        public void EnqueueTempUserSendTokenEmail(string emailAddress, string token);

        public void EnqueueTempUserResendTokenEmail(string emailAddress, string token);

    }
}
