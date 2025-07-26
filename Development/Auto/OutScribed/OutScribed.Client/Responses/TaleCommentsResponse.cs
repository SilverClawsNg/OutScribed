using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class TaleCommentsResponse
    {
        public Guid TaleId { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public bool More { get; set; }

        public int Counter { get; set; }

        public string CounterToString => Counter.ToString().GetCounts();

        public SortTypes Sort { get; set; }

        public List<TaleCommentSummary>? Comments { get; set; }
    }
}
