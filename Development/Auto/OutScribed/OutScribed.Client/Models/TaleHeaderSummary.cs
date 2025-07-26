using OutScribed.Client.Enums;

namespace OutScribed.Client.Models
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

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");
    }
}
