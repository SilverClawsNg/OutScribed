using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Globals;

namespace OutScribed.Client.Models
{
    public class TaleSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public Guid WriterId { get; set; }

        public string WriterUsername { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public string? PhotoUrlToString => PhotoUrl == null ? null : string.Format("{0}{1}{2}", Url, "outscribed/tales/", PhotoUrl);

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public string TaleUrl { get; set; } = default!;

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");

        public int Views { get; set; }

        public string ViewsToString => Views.ToString().GetCounts();

        public int Followers { get; set; }

        public string FollowersToString => Followers.ToString().GetCounts();

        public int Comments { get; set; }

        public string CommentsToString => Comments.ToString().GetCounts();

        public int Likes { get; set; }

        public string LikesToString => Likes.ToString().GetCounts();

        public int Hates { get; set; }

        public string HatesToString => Hates.ToString().GetCounts();

        public int Threads { get; set; }

        public string ThreadsToString => Threads.ToString().GetCounts();

        public bool IsFollowingTale { get; set; }

    }
}
