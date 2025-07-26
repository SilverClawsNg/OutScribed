using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Globals;

namespace OutScribed.Client.Models
{
    public class ThreadsSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public Guid ThreaderId { get; set; }

        public string ThreaderUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public string PhotoUrlToString => string.Format("{0}{1}{2}", Url, "images/threads/", PhotoUrl);

        public string Summary { get; set; } = default!;

        public string ThreadUrl { get; set; } = default!;

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

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

        public bool IsFollowingThread { get; set; }

    }
}
