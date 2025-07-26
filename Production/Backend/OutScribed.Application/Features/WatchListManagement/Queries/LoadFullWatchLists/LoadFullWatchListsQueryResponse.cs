using OutScribed.Application.Features.WatchListManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.WatchListManagement.Queries.LoadFullWatchLists
{

    public class LoadFullWatchListsQueryResponse
    {

        public Categories? Category { get; set; }

        public Countries? Country { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<WatchListSummary>? WatchLists { get; set; }

    }

}