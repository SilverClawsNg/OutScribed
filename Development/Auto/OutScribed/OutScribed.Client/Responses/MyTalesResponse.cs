using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class MyTalesResponse
    {
        public TaleStatuses? Status { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public string CounterToString => Counter.ToString().GetCounts();

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<TaleDraftSummary>? Tales { get; set; }
    }
}
