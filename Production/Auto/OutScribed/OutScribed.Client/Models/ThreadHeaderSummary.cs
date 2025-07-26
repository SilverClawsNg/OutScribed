using OutScribed.Client.Enums;

namespace OutScribed.Client.Models
{
    public class ThreadHeaderSummary
    {

        public Guid Id { get; set; }

        public Guid ThreaderId { get; set; }

        public string ThreaderUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? ThreadUrl { get; set; }

        public bool IsFollowingThread { get; set; }

        public string? Summary { get; set; }

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");
    }
}
