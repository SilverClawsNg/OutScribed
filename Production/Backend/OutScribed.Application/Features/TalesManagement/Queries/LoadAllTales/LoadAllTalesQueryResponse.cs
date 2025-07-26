using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadAllTales
{

    public class LoadAllTalesQueryResponse
    {

        public Categories? Category { get; set; }

        public Countries? Country { get; set; }

        public string? Username { get; set; }

        public Guid? WatchlistId { get; set; }

        public string? Keyword { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Tag { get; set; }

        public List<TaleSummary>? Tales { get; set; }

    }

}