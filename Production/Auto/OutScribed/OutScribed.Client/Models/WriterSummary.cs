using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Globals;

namespace OutScribed.Client.Models
{
    public class WriterSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public string Username { get; set; } = default!;

        public string? DisplayPhoto { get; set; }

        public string? DisplayPhotoToString => DisplayPhoto == null ? "/images/users/placeholder.jpeg" : string.Format("{0}{1}{2}", Url, "outscribed/users/", DisplayPhoto);

        public int Tales { get; set; }

        public string TalesToString => Tales.ToString().GetCounts();

        public int Followers { get; set; }

        public string FollowersToString => Followers.ToString().GetCounts();

        public bool IsFollowingUser { get; set; }

        public bool IsHidden { get; set; }

        public Countries Country { get; set; }

        public string CountryToString => Country.ToString().Replace("_", " ");

        public DateTime WriterDate { get; set; }

        public string WriterDateToString => WriterDate.ToString("MMMM dd yyyy");

    }
}
