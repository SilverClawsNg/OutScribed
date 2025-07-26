using System.Net.Mail;
using OutScribed.SharedKernel.Interfaces;

namespace OutScribed.Infrastructure.Providers
{
    public class EmailSender : IEmailSender
    {
        //private readonly IConfiguration _config;

        //public EmailSender(IConfiguration config)
        //{
        //    _config = config;
        //}

        public async Task SendOtpAsync(string email, int otpCode)
        {
            //var message = new MailMessage();
            //message.To.Add(email);
            //message.Subject = "Your OutScribed Verification Code";
            //message.Body = $"Your code is: {otpCode}";
            //message.From = new MailAddress(_config["Email:From"]);

            //using var smtp = new SmtpClient(_config["Email:SmtpServer"]);
            //smtp.Port = int.Parse(_config["Email:Port"]);
            //smtp.Credentials = new System.Net.NetworkCredential(
            //    _config["Email:Username"],
            //    _config["Email:Password"]
            //);
            //smtp.EnableSsl = true;

            //await smtp.SendMailAsync(message);
        }
    }
}