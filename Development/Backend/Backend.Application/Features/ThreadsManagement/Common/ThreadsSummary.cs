using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Common
{
    public class ThreadsSummary
    {
        public Guid Id { get; set; }

        public Guid ThreaderId { get; set; }

        public string ThreadUrl { get; set; } = default!;

        public string ThreaderUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

        public int Views { get; set; }

        public int Followers { get; set; }

        public int Comments { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public List<string>? Tags { get; set; }

        public bool IsFollowingThread { get; set; }

    }
}
