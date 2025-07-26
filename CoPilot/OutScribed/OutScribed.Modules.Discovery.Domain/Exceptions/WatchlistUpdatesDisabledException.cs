namespace OutScribed.Modules.Discovery.Domain.Exceptions
{
   
    public class WatchlistUpdatesDisabledException : Exception
    {

        public Ulid WatchlistId { get; }

        public string UpdateType { get; } = string.Empty;

        public WatchlistUpdatesDisabledException() : base("Post publication updates has been disabled on this watchlist.") { }

        public WatchlistUpdatesDisabledException(string message) : base(message) { }

        public WatchlistUpdatesDisabledException(string message, Exception innerException) : base(message, innerException) { }

        public WatchlistUpdatesDisabledException(Guid watchlistId, string updateType)
            : base($"The post publication update '{updateType}' has been disabled on the Watchlist '{watchlistId}'.")
        {
            WatchlistId = watchlistId;
            UpdateType = updateType;
        }

        public WatchlistUpdatesDisabledException(Guid watchlistId, string updateType, string message)
            : base(message)
        {
            WatchlistId = watchlistId;
            UpdateType = updateType;
        }
    }

}
