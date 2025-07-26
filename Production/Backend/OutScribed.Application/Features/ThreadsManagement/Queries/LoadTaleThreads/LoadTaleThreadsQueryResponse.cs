using OutScribed.Application.Features.ThreadsManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadTaleThreads
{

    public class LoadTaleThreadsQueryResponse
    {

        public Categories? Category { get; set; }

        public Guid TaleId { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<ThreadsSummary>? Threads { get; set; }

    }

}