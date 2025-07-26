using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Common
{
    public class TaleHistorySummary
    {
        public DateTime Date { get; set; }

        public TaleStatuses Status { get; set; }

        public Guid AdminId { get; set; }

        public string AdminUsername { get; set; } = default!;

        public string? Reasons { get; set; }

    }
}
