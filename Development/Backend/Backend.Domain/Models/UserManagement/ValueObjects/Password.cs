using Backend.Domain.Abstracts;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;
using Backend.Domain.Exceptions;

namespace Backend.Domain.Models.UserManagement.ValueObjects
{
    public sealed class Password : ValueObject
    {

        public string Value { get; init; }

        public string Salt { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Password() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Password(string value, string salt)
        {
            Value = value;
            Salt = salt;
        }

        /// <summary>
        /// Creates a new password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Result<Password> Create(string password)
        {
            byte[] salt = GenerateSalt();

            string saltToString = Convert.ToBase64String(salt);

            byte[] hashedPassword = GetHashedValue(password, salt);

            string hashedPasswordToString = Convert.ToBase64String(hashedPassword);

            return new Password(hashedPasswordToString, saltToString);
        }

        private static byte[] GetHashedValue(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);

            byte[] hashedPassword = pbkdf2.GetBytes(32); // 32 bytes = 256 bits

            return hashedPassword;
        }

        private static byte[] GenerateSalt()
        {
            byte[] RandomValue = new byte[16];

            RandomNumberGenerator RndGen = RandomNumberGenerator.Create();

            RndGen.GetBytes(RandomValue);

            return RandomValue;
        }

        public bool VerifyHashedPassword(string inputPassword)
        {

            byte[] saltBytes = Convert.FromBase64String(Salt);

            byte[] userPasswordBytes = Convert.FromBase64String(Value);

            byte[] inputPasswordBytes = GetHashedValue(inputPassword, saltBytes);


            return CompareHashes(userPasswordBytes, inputPasswordBytes);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool CompareHashes(byte[] userPasswordBytes, byte[] inputPasswordBytes)
        {
            if (userPasswordBytes.Length != inputPasswordBytes.Length)
                return false;

            for (int i = 0; i < userPasswordBytes.Length; i++)
            {
                if (userPasswordBytes[i] != inputPasswordBytes[i])
                    return false;
            }

            return true;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;

            yield return Salt;

        }

    }

}
