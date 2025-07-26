using Backend.Domain.Enums;

namespace Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchList
{

    public class LoadFullWatchListQueryResponse
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string SourceUrl { get; set; } = default!;

        public string SourceText { get; set; } = default!;

        public int Tales { get; set; }

        public int Followers { get; set; }

        public int Count { get; set; }

        public bool IsFollowingWatchlist { get; set; }

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

    }

}