using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Queries
{
    public class ThreadHeaderSummary
    {
        public Guid Id { get; set; }

        public Guid ThreaderId { get; set; }

        public string ThreaderUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? ThreadUrl { get; set; }

        public bool IsFollowingThread { get; set; }

        public string? Summary { get; set; }

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }


    }
}
