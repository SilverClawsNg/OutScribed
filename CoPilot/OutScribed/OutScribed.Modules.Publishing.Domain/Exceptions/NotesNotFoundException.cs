namespace OutScribed.Modules.Publishing.Domain.Exceptions
{
   
    public class NotesNotFoundException : Exception
    {

        public Ulid TaleId { get; }

        public NotesNotFoundException() : base("Required notes were not included.") { }

        public NotesNotFoundException(string message) : base(message) { }

        public NotesNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public NotesNotFoundException(Ulid taleId)
            : base($"The required notes to update Tale '{taleId}' were not found.")
        {
            TaleId = taleId;
        }

        public NotesNotFoundException(Ulid taleId, string message)
            : base(message)
        {
            TaleId = taleId;
        }
    }

}
