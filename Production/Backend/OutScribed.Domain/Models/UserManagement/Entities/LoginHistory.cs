using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;

namespace OutScribed.Domain.Models.UserManagement.Entities
{
    public class LoginHistory : Entity
    {

        public Guid UserId { get; private set; }

        public DateTime ActiveDate { get; private set; }

        public string IpAddress { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private LoginHistory() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private LoginHistory
            (DateTime activeDate, string ipAddress) : base(Guid.NewGuid())
        {
            ActiveDate = activeDate;
            IpAddress = ipAddress;
        }

        /// <summary>
        /// Creates a new login history
        /// </summary>
        /// <param name="ActiveDate"></param>
        /// <param name="IpAddress"></param>
        /// <returns></returns>
        public static Result<LoginHistory> Create(DateTime ActiveDate, string IpAddress)
        {

            return new LoginHistory(ActiveDate, IpAddress);
        }

    }

}
