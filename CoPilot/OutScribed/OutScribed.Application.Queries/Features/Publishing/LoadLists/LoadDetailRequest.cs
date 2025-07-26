using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadLists
{
    public class LoadListsRequest
    {

        public Ulid? WatchlistId { get; set; }

        public string? Tag { get; set; }

        public Category? Category { get; set; }

        public Country? Country { get; set; }

        public string? Username { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
