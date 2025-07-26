namespace OutScribed.Application.Features.WatchListManagement.Commands.LinkWatchList
{
    public class LinkWatchListRequest
    {
        public Guid WatchListId { get; set; }

        public Guid TaleId { get; set; }

    }
}
