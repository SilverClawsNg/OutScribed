using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Domain.Models
{

    public class Account : AggregateRoot
    {

        public DateTime RegisteredAt { get; private set; }

        public string EmailAddress { get; private set; } = default!;

        public string Username { get; set; } = default!;

        public Password Password { get; private set; } = default!;

        public OneTimeToken? OTP { get; private set; }

        public RefreshToken? RefreshToken { get; private set; }


        public Profile? Profile { get; private set; }

        public Writer? Writer { get; private set; }

        public Admin? Admin { get; private set; }

        public bool DoNotResendOtp { get; init; }


        private readonly List<Contact> _contacts = [];

        public IReadOnlyList<Contact> Contacts => [.. _contacts];


        private readonly List<LoginHistory> _loginHistories = [];

        public IReadOnlyList<LoginHistory> LoginHistories => [.. _loginHistories];


        private readonly List<Notification> _notifications = [];

        public IReadOnlyList<Notification> Notifications => [.. _notifications];

        private Account() { }

        private Account(Ulid id, string emailAddress, string username, Password password,
           Profile profile, Notification notification)
           : base(id)
        {
            EmailAddress = emailAddress;
            Username = username;
            Password = password;
            Profile = profile;
            RegisteredAt = DateTime.UtcNow;
            DoNotResendOtp = false;
            _contacts = [];
            _loginHistories = [];
            _notifications = [notification];
        }

        public static Account Create(Ulid id, string emailAddress, string username,
          string password)
        {

            var invalidFields = Validator.GetInvalidFields(
              [
                  new("Email Address", emailAddress, minLength: 3, maxLength: 255),
                  new("Username", username, minLength: 3, maxLength: 20),
                  new("Password", password, minLength: 8),
                   new("Account ID", id)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            //Create a notification
          
            return new Account(
                id,
                emailAddress, 
                username, 
                Password.Create(password), 
                Profile.Create(),
                Notification.Create("Account Created", NotificationType.Account_Created, 
                ContentType.Account)
                );

        }

    }
}
