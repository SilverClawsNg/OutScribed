using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;
using System.Security.Cryptography;

namespace OutScribed.Domain.Models.UserManagement.ValueObjects
{
    public sealed class RefreshToken : ValueObject
    {
        public string Value { get; init; }

        public DateTime ExpiryDate { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private RefreshToken() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private RefreshToken(string value, DateTime date)
        {
            Value = value;
            ExpiryDate = date;
        }

        /// <summary>
        /// Creates a new email address
        /// </summary>
        /// <param name="value"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static Result<RefreshToken> Create(DateTime date)
        {

            byte[] RandomValue = new byte[16];

            RandomNumberGenerator RndGen = RandomNumberGenerator.Create();

            RndGen.GetBytes(RandomValue);

            var value = Convert.ToBase64String(RandomValue);

            return new RefreshToken(value, date);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;

            yield return ExpiryDate;

        }
    }

}
