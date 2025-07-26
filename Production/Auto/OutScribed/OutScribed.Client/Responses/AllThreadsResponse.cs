using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class AllThreadsResponse
    {
        public Categories? Category { get; set; }

        public Guid? TaleId { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public string CounterToString => Counter.ToString().GetCounts();

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<ThreadsSummary>? Threads { get; set; }
    }
}
