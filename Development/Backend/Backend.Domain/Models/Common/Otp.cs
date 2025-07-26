using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;

namespace Backend.Domain.Models.Common
{
    public class Otp : ValueObject
    {

        public int Password { get; init; }

        public DateTime Date { get; init; }

        private Otp() { }

        private Otp(int password)
        {
            Password = password;
            Date = DateTime.UtcNow;
        }

        /// <summary>
        /// Creates a new one time password
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        public static Result<Otp> Create()
        {
            var password = new Random().Next(123456, 987654);

            return new Otp(password);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Password;
            yield return Date;

        }
    }

}
