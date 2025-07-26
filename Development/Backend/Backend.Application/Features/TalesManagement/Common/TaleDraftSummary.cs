using Backend.Domain.Enums;
namespace Backend.Application.Features.TalesManagement.Common
{
    public class TaleDraftSummary
    {
        public Guid Id { get; set; }

        public Guid WriterId { get; set; }

        public string WriterUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? TaleUrl { get; set; }

        public string? Summary { get; set; }

        public string? PhotoUrl { get; set; }

        public List<string>? Tags { get; set; }

        public string? Details { get; set; }

        public TaleStatuses Status { get; set; }

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

        public List<TaleHistorySummary> History { get; set; } = default!;

    }
}
