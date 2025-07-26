using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.TempUserManagement.Entities
{

    public class TempUser : Entity, IAggregateRoot
    {

        public static int OtpExpiryInMinutes => 5;

        public Label? EmailAddress { get; private set; }

        public Label? PhoneNumber { get; private set; }

        public Otp Otp { get; private set; }

        public bool DoNotResendOtp { get; private set; }

        public bool Verified { get; private set; }



#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private TempUser() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private TempUser(Label? emailAddress, Label? phoneNumber, Otp otp)
           : base(Guid.NewGuid())
        {
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Otp = otp;
            DoNotResendOtp = false;
        }

        public static Result<TempUser> Create(ContactTypes type, string? emailAddress, string? phoneNumber)
        {

            Result<Label>? emailResult = null;

            Result<Label>? phoneResult = null;

            if(type == ContactTypes.Email)
            {
                emailResult = Label.Create(emailAddress, "Email Address", 56);

                if (emailResult.IsFailure)
                    return emailResult.Error;
            }
            else if(type == ContactTypes.Telephone)
            {
                phoneResult = Label.Create(phoneNumber, "Phone Number", 24);

                if (phoneResult.IsFailure)
                    return phoneResult.Error;
            }
              

            var otpResult = Otp.Create();

            return new TempUser(emailResult?.Value, phoneResult?.Value, otpResult.Value);

        }

        public Result<int> AddOtp()
        {

            var otpResult = Otp.Create();

            Otp = otpResult.Value;

            DoNotResendOtp = false;

            return otpResult.Value.Password;
        }

        /// <summary>
        /// Send account activation otp
        /// </summary>
        /// <param name="otpValue"></param>
        public Result<int> SendOtp()
        {

            if (Otp != null && DoNotResendOtp == true && DateTime.UtcNow.Subtract(Otp.Date).Minutes < 30)
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                      Title: "Too Many OTP Sends",
                                        Description: "Too many one-time passwords has been resent. Wait at least 30 minutes before trying again.");

            return AddOtp();
        }

        /// <summary>
        /// Send account activation otp
        /// </summary>
        /// <param name="otpValue"></param>
        public Result<int> ResendOtp()
        {

            if (Otp != null && DoNotResendOtp == true && DateTime.UtcNow.Subtract(Otp.Date).Minutes < 30)
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                      Title: "Too Many OTP Sends",
                                        Description: "Too many one-time passwords has been resent. Wait at least 30 minutes before trying again.");

            if (Otp != null && DateTime.UtcNow.Subtract(Otp.Date).Minutes < 5)
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                      Title: "OTP Time Not Expired",
                                        Description: "Time in-between OTP sends has not expired. Wait at least 5 minutes before trying again.");

            return AddOtp();
        }

        /// <summary>
        /// Do not resend otp until 30 minutes
        /// </summary>
        public Result<bool> SetDoNotResendOtp()
        {

            DoNotResendOtp = true;

            return true;
        }

        public Result<bool> SetVerified()
        {

            Verified = true;

            return true;
        }
    }

}
