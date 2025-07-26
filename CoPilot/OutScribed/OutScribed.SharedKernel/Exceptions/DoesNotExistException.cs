namespace OutScribed.SharedKernel.Exceptions
{
   
    public class DoesNotExistException : Exception
    {

        public Ulid Id { get; }

        public string Type { get; } = string.Empty;


        public string UpdateType { get; } = string.Empty;

        public DoesNotExistException() : base("Item does not exist.") { }

        public DoesNotExistException(string message) : base(message) { }

        public DoesNotExistException(string message, Exception innerException) : base(message, innerException) { }

        public DoesNotExistException(Ulid id, string type)
            : base($"The '{type}' with this ID: '{id} does not exist'.")
        {
            Id = id;
            Type = type;
        }

        public DoesNotExistException(Ulid id, string type, string message)
            : base(message)
        {
            Id = id;
            Type = type;
        }

    }

}
