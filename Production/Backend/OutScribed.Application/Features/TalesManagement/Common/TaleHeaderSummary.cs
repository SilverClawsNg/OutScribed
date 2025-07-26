using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Common
{
    public class TaleHeaderSummary
    {
        public Guid Id { get; set; }

        public Guid WriterId { get; set; }

        public string WriterUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? TaleUrl { get; set; }

        public bool IsFollowingTale { get; set; }

        public string? Summary { get; set; }

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }


    }
}
