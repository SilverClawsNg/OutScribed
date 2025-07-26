namespace OutScribed.Modules.Publishing.Domain.Exceptions
{
   
    public class TaleUpdatesDisabledException : Exception
    {

        public Ulid TaleId { get; }

        public string UpdateType { get; } = string.Empty;

        public TaleUpdatesDisabledException() : base("Post publication updates has been disabled on this tale.") { }

        public TaleUpdatesDisabledException(string message) : base(message) { }

        public TaleUpdatesDisabledException(string message, Exception innerException) : base(message, innerException) { }

        public TaleUpdatesDisabledException(Ulid taleId, string updateType)
            : base($"The post publication update '{updateType}' has been disabled on the Tale '{taleId}'.")
        {
            TaleId = taleId;
            UpdateType = updateType;
        }

        public TaleUpdatesDisabledException(Ulid taleId, string updateType, string message)
            : base(message)
        {
            TaleId = taleId;
            UpdateType = updateType;
        }
    }

}
