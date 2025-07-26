using OutScribed.Application.Features.ThreadsManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadDrafts
{

    public class LoadThreadDraftsQueryResponse
    {


        public Categories? Category { get; set; }

        public Countries? Country { get; set; }

        public bool? IsOnline { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<ThreadDraftSummary>? Threads { get; set; }

    }

}