using OutScribed.Application.Features.ThreadsManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThread
{

    public class LoadThreadQueryResponse
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public Guid ThreaderId { get; set; } = default!;

        public string ThreaderUsername { get; set; } = default!;

        public int ThreaderProfileViews { get; set; } = default!;

        public int ThreaderFollowers { get; set; }

        public bool IsFollowingThreader { get; set; }

        public string TaleUrl { get; set; } = default!;

        public string TaleTitle { get; set; } = default!;

        public string Details { get; set; } = default!;

        public DateTime Date { get; set; }

        public Categories Category { get; set; }

        public Countries? Country { get; set; }

        public int Views { get; set; }

        public int Followers { get; set; }

        public int CommentsCount { get; set; }

        public int Likes { get; set; }

        public int Hates { get; set; }

        public int Shares { get; set; }

        public int Flags { get; set; }

        public List<string>? Tags { get; set; }

        public bool IsFollowingThread { get; set; }

        public bool HasFlagged { get; set; }

        public RateTypes? MyRatings { get; set; }

        public List<ThreadAddendumSummary>? Addendums { get; set; }

    }

}