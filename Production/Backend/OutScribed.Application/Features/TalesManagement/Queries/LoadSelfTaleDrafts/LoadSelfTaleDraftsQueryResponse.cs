using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadSelfTaleDrafts
{

    public class LoadSelfTaleDraftsQueryResponse
    {

        public TaleStatuses? Status { get; set; }

        public Categories? Category { get; set; }

        public Countries? Country { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<TaleDraftSummary>? Tales { get; set; }

    }

}