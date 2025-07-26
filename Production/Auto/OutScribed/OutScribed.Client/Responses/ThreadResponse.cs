using AngleSharp.Dom;
using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Globals;
using OutScribed.Client.Models;
using System.Web;

namespace OutScribed.Client.Responses
{
    public class ThreadResponse
    {
        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public string PhotoUrlToString => string.Format("{0}{1}{2}", Url, "outscribed/threads/", PhotoUrl);

        public string ThreadUrl { get; set; } = default!;

        public Guid ThreaderId { get; set; } = default!;

        public string ThreaderUsername { get; set; } = default!;

        public string ThreaderTitle { get; set; } = default!;

        public int ThreaderFollowers { get; set; }

        public string ThreaderFollowersToString => ThreaderFollowers.ToString().GetCounts();

        public int ThreaderProfileViews { get; set; }

        public string ThreaderProfileViewsToString => ThreaderProfileViews.ToString().GetCounts();

        public bool IsFollowingThreader { get; set; }

        public string TaleUrl { get; set; } = default!;

        public string TaleTitle { get; set; } = default!;

        public string Details { get; set; } = default!;

        public string DetailsDecoded => HttpUtility.HtmlDecode(Details);

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");

        public List<string>? Tags { get; set; }

        public int Views { get; set; }

        public string ViewsToString => Views.ToString().GetCounts();

        public int Followers { get; set; }

        public string FollowersToString => Followers.ToString().GetCounts();

        public int CommentsCount { get; set; }

        public string CommentsCountToString => CommentsCount.ToString().GetCounts();

        public int Likes { get; set; }

        public string LikesToString => Likes.ToString().GetCounts();

        public int Hates { get; set; }

        public string HatesToString => Hates.ToString().GetCounts();

        public int Shares { get; set; }

        public string SharesToString => Shares.ToString().GetCounts();

        public int Flags { get; set; }

        public string FlagsToString => Flags.ToString().GetCounts();

        public bool IsFollowingThread { get; set; }

        public bool HasFlagged { get; set; }

        public RateTypes? MyRatings { get; set; }

        public string? MyRatingsToString => MyRatings == RateTypes.Like ? "You already liked this tale"
         : MyRatings == RateTypes.Hate ? "You already hated this tale"
         : null;

        public List<ThreadAddendumSummary>? Addendums { get; set; }

    }
}
