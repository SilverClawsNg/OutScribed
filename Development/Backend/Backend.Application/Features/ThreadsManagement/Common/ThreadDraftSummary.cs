using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Common
{
    public class ThreadDraftSummary
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string? Summary { get; set; }

        public string? Details { get; set; }

        public string? PhotoUrl { get; set; }

        public string ThreadUrl { get; set; } = default!;

        public string TaleTitle { get; set; } = default!;

        public string TaleUrl { get; set; } = default!;

        public List<string>? Tags { get; set; }

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

        public bool IsOnline { get; set; }

        public List<ThreadAddendumSummary>? Addendums { get; set; }

    }
}
