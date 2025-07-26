using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Events;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.Common;
using OutScribed.Domain.Models.UserManagement.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Domain.Models.UserManagement.Entities
{
    public class Account : Entity, IAggregateRoot
    {

        public static int OtpExpiryInMinutes => 5;

        public DateTime RegisteredDate { get; private set; }

        public Label? EmailAddress { get; private set; }

        public Label? PhoneNumber { get; private set; }

        public Label Username { get; set; }

        public Password Password { get; private set; }

        public Otp? Otp { get; private set; }

        public bool DoNotResendOtp { get; private set; }

        public RefreshToken? RefreshToken { get; private set; }


        public int Views { get; private set; }

        public Profile Profile { get; private set; }

        public Admin? Admin { get; private set; }


        public RoleTypes Role { get; private set; }


        private readonly List<Contact> _Contacts = [];

        public IReadOnlyList<Contact> Contacts => [.. _Contacts];


        private readonly List<AccountFollower> _Followers = [];

        public IReadOnlyList<AccountFollower> Followers => [.. _Followers];


        private readonly List<LoginHistory> _LoginHistories = [];

        public IReadOnlyList<LoginHistory> LoginHistories => [.. _LoginHistories];


        private readonly List<Activity> _Activities = [];

        public IReadOnlyList<Activity> Activities => [.. _Activities];


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Account() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Account(Label? emailAddress, Label? phoneNumber, Label username, Password password,
            Profile profile, List<Activity> activities)
            : base(Guid.NewGuid())
        {
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Username = username;
            Password = password;
            Profile = profile;
            RegisteredDate = DateTime.UtcNow;
            DoNotResendOtp = false;
            Role = RoleTypes.None;
            _Followers = [];
            _Contacts = [];
            _LoginHistories = [];
            _Activities = activities;
        }

        /// <summary>
        /// Delegates creation of a new account
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public static Result<Account> Create(ContactTypes type, string? emailAddress, string? phoneNumber, string username,
            string password)
        {

            Result<Label>? emailResult = null;

            Result<Label>? phoneResult = null;

            if (type == ContactTypes.Email)
            {
                emailResult = Label.Create(emailAddress, "Email Address", 56);

                if (emailResult.IsFailure)
                    return emailResult.Error;
            }
            else if (type == ContactTypes.Telephone)
            {
                phoneResult = Label.Create(phoneNumber, "Phone Number", 24);

                if (phoneResult.IsFailure)
                    return phoneResult.Error;
            }

            var usernameResult = Label.Create(username, "Username", 20);

            if (usernameResult.IsFailure)
                return usernameResult.Error;

            var passwordResult = Password.Create(password);

            if (passwordResult.IsFailure)
                return passwordResult.Error;

            var profileResult = Profile.Create();

            if (profileResult.IsFailure)
                return profileResult.Error;


            //Save activity
            var activities = new List<Activity>();

            var activityResult = Activity.Create("register", ActivityTypes.Account,
                ActivityConstructorTypes.Created_Account, DateTime.UtcNow);

            if (activityResult.IsFailure == false)
                activities.Add(activityResult.Value);

            return new Account(emailResult?.Value, phoneResult?.Value, usernameResult.Value,
                passwordResult.Value, profileResult.Value, activities);

        }

        /// <summary>
        /// Confirms a login attempted
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result<string> ConfirmLogin(string password, string ipAddress)
        {

            var date = DateTime.UtcNow;

            var loginResult = LoginHistory.Create(date, ipAddress);

            if (loginResult.IsFailure == false)
                _LoginHistories.Add(loginResult.Value);

            if (Password.VerifyHashedPassword(password) == false)
                return new Error(Code: StatusCodes.Status401Unauthorized,
                                Title: "Invalid Password",
                                Description: $"Provided account credentials: '{password}' does not match a user.");


            var activityResult = Activity.Create("login", ActivityTypes.Account,
                ActivityConstructorTypes.Signed_In, date);

            if (activityResult.IsFailure == false)
                _Activities.Add(activityResult.Value);

            return UpdateRefreshToken();

        }

        /// <summary>
        /// Creates a new refresh token
        /// </summary>
        /// <returns></returns>
        public Result<string> UpdateRefreshToken()
        {

            var refreshToken = RefreshToken.Create(DateTime.UtcNow.AddMinutes(720));

            if (refreshToken.IsFailure != false)
                return new Error(Code: StatusCodes.Status401Unauthorized,
                               Title: "Generation Error",
                               Description: $"Error occured while generating refresh token.");

            RefreshToken = refreshToken.Value;

            return refreshToken.Value.Value;

        }

        /// <summary>
        /// Deletes refresh token
        /// </summary>
        /// <returns></returns>
        public Result<bool> RemoveRefreshToken()
        {

            RefreshToken = null;

            return true;
        }

        /// <summary>
        /// Adds a new otp
        /// </summary>
        /// <param name="otpValue"></param>
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
        /// <param name="otpValue"></param>
        public Result<bool> SetDoNotResendOtp()
        {

            DoNotResendOtp = true;

            return true;
        }

        /// <summary>
        /// Updates password of an existing user
        /// </summary>
        /// <param name="password"></param>
        public Result<bool> UpdatePassword(string oldPassword, string newPassword)
        {

            if (Password.VerifyHashedPassword(oldPassword) == false)
                return new Error(Code: StatusCodes.Status401Unauthorized,
                                  Title: "Invalid Password",
                                  Description: $"Provided account credentials: '{oldPassword}' does not match a user.");

            var passwordResult = Password.Create(newPassword);

            if (passwordResult.Value == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                          Title: "Encrytion Error",
                          Description: $"Error occured while encrypting password.");

            Password = passwordResult.Value;


            var activityResult = Activity.Create("password", ActivityTypes.Account,
                ActivityConstructorTypes.Changed_Password, DateTime.UtcNow);

            if (activityResult.IsFailure == false)
                _Activities.Add(activityResult.Value);

            return true;

        }

        /// <summary>
        /// Updates password of an existing user
        /// </summary>
        /// <param name="password"></param>
        public Result<bool> ResetPassword(string password, int otp)
        {

            //Check if otp has expired
            if (Otp is null || DateTime.UtcNow.Subtract(Otp.Date).Minutes > OtpExpiryInMinutes)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                 Title: "Expired OTP",
                                 Description: $"The one-time password: '{otp}' has expired.");

            //Compare to user's otp
            if (otp != Otp.Password)
                return new Error(Code: StatusCodes.Status500InternalServerError,
                                 Title: "Invalid OTP",
                                 Description: $"The one-time password: '{otp}' is not valid.");

            var passwordResult = Password.Create(password);

            if (passwordResult.Value == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                            Title: "Encrytion Error",
                            Description: $"Error occured while encrypting password.");

            Password = passwordResult.Value;

            RefreshToken = null;

            var activityResult = Activity.Create("password", ActivityTypes.Account,
                ActivityConstructorTypes.Reset_Password, DateTime.UtcNow);

            if (activityResult.IsFailure == false)
                _Activities.Add(activityResult.Value);

            return true;

        }

        /// <summary>
        /// Updates contact
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Result<bool> AddRole(RoleTypes? role)
        {

            if (Role != role)
            {

                if (role == null)
                    return new Error(Code: StatusCodes.Status400BadRequest,
                                        Title: "Null Role",
                                         Description: "No role was found.");

                if (!Enum.IsDefined(typeof(RoleTypes), role))
                    return new Error(Code: StatusCodes.Status400BadRequest,
                                        Title: "Invalid Role",
                                         Description: $"The role with title: {role} is invalid.");

                Role = (RoleTypes)role;

                var type = role == RoleTypes.None ? ActivityConstructorTypes.Removed_Role
                    : ActivityConstructorTypes.Assigned_Role;

                var activityResult = Activity.Create(role.ToString()!, ActivityTypes.Account,
                    type, DateTime.UtcNow);

                if (activityResult.IsFailure == false)
                    _Activities.Add(activityResult.Value);

            }

            return true;

        }

        public Result<bool> UpdateProfile(string title, string bio, string? emailAddress,
            string? phoneNumber, bool isHidden, string? photourl)
        {
            var titleResult = Profile.Update(title, bio, photourl, isHidden);

            if (titleResult.IsFailure)
                return titleResult.Error;

            if (emailAddress != null)
            {
                var emailResult = Label.Create(emailAddress, "Email Address", 56);

                if (emailResult.IsFailure)
                    return emailResult.Error;

                EmailAddress = emailResult.Value;
            }

            if (phoneNumber != null)
            {
                var phoneResult = Label.Create(phoneNumber, "Phone Number", 24);

                if (phoneResult.IsFailure)
                    return phoneResult.Error;

                PhoneNumber = phoneResult.Value;
            }


            var activityResult = Activity.Create("profile", ActivityTypes.Account,
                ActivityConstructorTypes.Update_Profile, DateTime.UtcNow);

            if (activityResult.IsFailure == false)
                _Activities.Add(activityResult.Value);


            return true;
        }

        public Result<bool> CreateWriterApplication(string address, Countries? country,
            string? applicationUrl)
        {

            if (Admin is not null)
            {
                if (Role == RoleTypes.None)
                    return new Error(Code: StatusCodes.Status400BadRequest,
                    Title: "Duplicate Applications",
                               Description: $"You cannot submit multiple applications.");

                else
                    return new Error(Code: StatusCodes.Status400BadRequest,
                               Title: "Application Unallowed",
                               Description: $"Application not allowed to users with assigned writers' role.");
            }

            var adminResult = Admin.Create(address, country, applicationUrl);

            if (adminResult.IsFailure)
                return adminResult.Error;

            Admin = adminResult.Value;

            var activityResult = Activity.Create("application", ActivityTypes.Account,
                ActivityConstructorTypes.Submitted_Application, DateTime.UtcNow);

            if (activityResult.IsFailure == false)
                _Activities.Add(activityResult.Value);

            return true;

        }

        public Result<bool> UpdateContact(ContactTypes type, string title)
        {

            ActivityConstructorTypes constructorType;

            var contact = _Contacts.Where(c => c.Type == type).FirstOrDefault();

            if (contact is null)
            {

                var contactResult = Contact.Create(type, title);

                if (contactResult.IsFailure)
                    return contactResult.Error;

                _Contacts.Add(contactResult.Value);

                constructorType = ActivityConstructorTypes.Added_Contact;

            }
            else
            {
                contact?.Update(title);

                constructorType = ActivityConstructorTypes.Updated_Contact;

            }

            var activityResult = Activity.Create(type.ToString().Replace("_", " ").ToLower(), ActivityTypes.Account,
                constructorType, DateTime.UtcNow);

            if (activityResult.IsFailure == false)
                _Activities.Add(activityResult.Value);

            return true;

        }

        public Result<int> SaveFollow(Guid followerId, string? username, bool option)
        {


            var date = DateTime.UtcNow;

            var followed = _Followers.Where(c => c.FollowerId == followerId).FirstOrDefault();

            if (option == false)
            {

                if (followed is null)
                    return new Error(
                                   Code: StatusCodes.Status400BadRequest,
                                   Title: $"Operation Unallowed",
                                   Description: $"User is not currently following account."
                                   );

                _Followers.Remove(followed);

                AddDomainEvent(new ActivityUpdatedEvent(date, followerId, $"{Username.Value}",
            ActivityTypes.Account, ActivityConstructorTypes.UnFollowing_Account));

            }
            else
            {

                if (followed is not null)
                    return new Error(
                                   Code: StatusCodes.Status400BadRequest,
                                   Title: $"Operation Unallowed",
                                   Description: $"User is already following account."
                                   );

                var followResult = AccountFollower.Create(followerId);

                if (followResult.IsFailure)
                    return followResult.Error;

                _Followers.Add(followResult.Value);


                AddDomainEvent(new ActivityUpdatedEvent(date, followerId, $"{Username.Value}",
                    ActivityTypes.Account, ActivityConstructorTypes.Following_Account));

                AddDomainEvent(new ActivityUpdatedEvent(date, Id, $"{username}",
                   ActivityTypes.Account, ActivityConstructorTypes.FollowBy_Account));


            }

            return _Followers.Count;
        }

        public void UpdateViews()
        {
            Views++;
        }

        public void MarkAsRead(Guid activityId)
        {
            var activity = _Activities.FirstOrDefault(c => c.Id == activityId);

            activity?.MarkAsRead();
        }

        public Result<bool> AddActivity(string details, ActivityTypes type,
            ActivityConstructorTypes constructorType, DateTime date)
        {
            var activityResult = Activity.Create(details, type, constructorType, date);

            if (activityResult.IsFailure)
                return activityResult.Error;

            _Activities.Add(activityResult.Value);

            return true;
        }

    }


}
