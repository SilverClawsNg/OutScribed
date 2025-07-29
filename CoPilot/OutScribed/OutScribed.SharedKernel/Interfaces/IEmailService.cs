namespace OutScribed.SharedKernel.Interfaces
{
    public interface IEmailService
    {
        Task SendTempUserTokenEmailAsync(string emailAddress, string token);

        Task ResendTempUserTokenEmailAsync(string emailAddress, string token);

    }
}
