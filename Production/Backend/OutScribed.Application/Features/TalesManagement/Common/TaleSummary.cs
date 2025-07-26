using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Common
{
    public class TaleSummary
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string TaleUrl { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public Guid WriterId { get; set; }

        public string WriterUsername { get; set; } = default!;

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

        public int Views { get; set; }

        public int Followers { get; set; }

        public int Comments { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Shares { get; set; }

        public int Threads { get; set; }

        public bool IsFollowingTale { get; set; }

    }
}
