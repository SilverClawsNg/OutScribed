using System.Text;

namespace OutScribed.SharedKernel.DomainFieldValidation
{
    public class InvalidParametersException : Exception
    {
        public IReadOnlyList<InvalidParameters> Invalids { get; }

        // Private method to build the default message from Invalids
        private static string BuildDefaultMessage(IEnumerable<InvalidParameters> invalids)
        {
            if (invalids == null || !invalids.Any())
            {
                return "One or more required fields are missing or invalid.";
            }

            var sb = new StringBuilder("The following required fields are missing or invalid: ");
            sb.Append(string.Join(", ", invalids.Select(f => f.FieldName)));
            sb.Append('.');

            return sb.ToString();
        }

        // --- Constructors ---

        // Standard parameterless constructor
        public InvalidParametersException()
            : base(BuildDefaultMessage([]))
        {
            Invalids = [];
        }

        // Constructor with a custom message
        public InvalidParametersException(string message)
            : base(message)
        {
            Invalids = [];
        }

        // Constructor with a custom message and an inner exception
        public InvalidParametersException(string message, Exception innerException)
            : base(message, innerException)
        {
            Invalids = [];
        }

        // Key constructor: accepts a list of InvalidParameters
        // This is the constructor you'll primarily use when throwing the exception.
        public InvalidParametersException(IReadOnlyList<InvalidParameters> invalids, string? message = null)
            : base(message ?? BuildDefaultMessage(invalids))
        {
            Invalids = invalids ?? [];
        }

    }
}
