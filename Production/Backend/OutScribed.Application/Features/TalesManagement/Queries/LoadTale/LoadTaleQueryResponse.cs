using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTale
{

    public class LoadTaleQueryResponse
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string TaleUrl { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public string Details { get; set; } = default!;

        public Guid WriterId { get; set; }

        public string WriterUsername { get; set; } = default!;

        public int WriterProfileViews { get; set; } = default!;

        public int WriterFollowers { get; set; }

        public bool IsFollowingWriter { get; set; }

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

        public int Views { get; set; }

        public int Followers { get; set; }

        public int CommentsCount { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Threads { get; set; }

        public int Shares { get; set; }

        public int Flags { get; set; }

        public List<string>? Tags { get; set; }

        public bool IsFollowingTale { get; set; }

        public bool HasFlagged { get; set; }

        public RateTypes? MyRatings { get; set; }
        
    }

}