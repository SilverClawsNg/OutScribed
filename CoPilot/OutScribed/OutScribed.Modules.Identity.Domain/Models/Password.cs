using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class Password : ValueObject
    {

        public string Hash { get; init; } = default!;
        public string Salt { get; init; } = default!;

        private Password() { }

        private Password(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public static Password Create(string password)
        {
           
            var invalidFields = Validator.GetInvalidFields(
               [
                   new("Password", password, minLength: 8)
                 ]
              );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

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

            byte[] userPasswordBytes = Convert.FromBase64String(Hash);

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }
    }
   
}
