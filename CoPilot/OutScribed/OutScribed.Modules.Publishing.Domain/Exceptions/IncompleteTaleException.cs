namespace OutScribed.Modules.Publishing.Domain.Exceptions
{
   
    public class IncompleteTaleException : Exception
    {

        public Ulid TaleId { get; }

        public IncompleteTaleException() : base("Incomplete tales are ineligible for published.") { }

        public IncompleteTaleException(string message) : base(message) { }

        public IncompleteTaleException(string message, Exception innerException) : base(message, innerException) { }

        public IncompleteTaleException(Ulid taleId)
            : base($"The Tale '{taleId}' is incomplete and ineligible for publication.")
        {
            TaleId = taleId;
        }

        public IncompleteTaleException(Ulid taleId, string message)
            : base(message)
        {
            TaleId = taleId;
        }
    }

}
