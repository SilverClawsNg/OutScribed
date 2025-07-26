using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using OutScribed.Infrastructure.EmailGateway.ViewModels;

namespace OutScribed.Infrastructure.EmailGateway
{
    public class MailSender : IMailSender
    {

        #region Initialize

        private readonly IWebHostEnvironment _env;

        public MailSender([FromServices] IWebHostEnvironment env)
        {

            Email.DefaultSender = new SmtpSender(new SmtpClient()
            {
                Host = "smtp.zoho.com",
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Port = 587,
                Credentials = new NetworkCredential("info@outscribed.com", "SilverClaws_333666")
            });

            Email.DefaultRenderer = new RazorRenderer();

            _env = env;

        }
        private IFluentEmail Extract(string emailAddress, string subject, string view, object model)
        {

            return Email
                  .From("OutScribed<info@outscribed.com>")
                  .To(emailAddress)
                  .Subject(subject)
                  .UsingTemplateFromFile(Path.Combine(_env.ContentRootPath, view), model);

        }

        public async Task SendPasswordRecoveryOtpMail(string emailAddress, int otp)
        {
            await Extract(emailAddress,
            "Password recovery token...",
            "Views/Email/PasswordRecoveryToken.cshtml",
            new SendPasswordRecoveryOtpMailVM(emailAddress, otp))
            .SendAsync();

            await Task.CompletedTask;
        }

        public async Task SendResendPasswordRecoveryOtpMail(string emailAddress, int otp)
        {
            await Extract(emailAddress,
            "Password recovery token...",
            "Views/Email/ResendPasswordRecoveryToken.cshtml",
            new SendResendPasswordRecoveryOtpMailVM(emailAddress, otp))
            .SendAsync();

            await Task.CompletedTask;
        }

        public async Task SendVerificationOtpMail(string emailAddress, int otp)
        {
            await Extract(emailAddress,
            "Registration verification token...",
            "Views/Email/VerificationToken.cshtml",
            new SendVerificationOtpMailVM(emailAddress, otp))
            .SendAsync();

            await Task.CompletedTask;
        }

        public async Task SendResendVerificationOtpMail(string emailAddress, int otp)
        {
            await Extract(emailAddress,
            "Registration verification token...",
            "Views/Email/ResendVerificationToken.cshtml",
            new SendResendVerificationOtpMailVM(emailAddress, otp))
            .SendAsync();

            await Task.CompletedTask;
        }

        #endregion


    }
}
