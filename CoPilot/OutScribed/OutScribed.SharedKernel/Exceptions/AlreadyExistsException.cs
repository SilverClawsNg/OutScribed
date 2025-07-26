namespace OutScribed.SharedKernel.Exceptions
{
   
    public class AlreadyExistsException : Exception
    {

        public Ulid Id { get; }

        public string Type { get; } = string.Empty;


        public string UpdateType { get; } = string.Empty;

        public AlreadyExistsException() : base("Item already exists.") { }

        public AlreadyExistsException(string message) : base(message) { }

        public AlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

        public AlreadyExistsException(Ulid id, string type)
            : base($"The '{type}' with this ID: '{id} already exists'.")
        {
            Id = id;
            Type = type;
        }

        public AlreadyExistsException(Ulid id, string type, string message)
            : base(message)
        {
            Id = id;
            Type = type;
        }

    }

}
