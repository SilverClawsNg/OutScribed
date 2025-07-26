using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;

namespace OutScribed.Client.Models
{
    public class WatchListSummary
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string SourceUrl { get; set; } = default!;

        public string SourceText { get; set; } = default!;

        public int Tales { get; set; }

        public string TalesToString => Tales.ToString().GetCounts();

        public int Followers { get; set; }

        public string FollowersToString => Followers.ToString().GetCounts();

        public int Count { get; set; }

        public bool IsFollowingWatchlist { get; set; }

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");


    }
}
