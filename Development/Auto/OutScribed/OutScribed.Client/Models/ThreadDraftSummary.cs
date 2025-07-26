using OutScribed.Client.Enums;
using OutScribed.Client.Globals;
using System.Web;

namespace OutScribed.Client.Models
{
    public class ThreadDraftSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public List<string>? Tags { get; set; }

        public string? TagsToString => Tags == null ? null : string.Join(", ", Tags);

        public string? Summary { get; set; }

        public string? Details { get; set; }

        public string? DetailsDecoded => Details == null ? null : HttpUtility.HtmlDecode(Details);

        public string? PhotoUrl { get; set; }

        public string? PhotoUrlToString => PhotoUrl == null ? null : string.Format("{0}{1}{2}", Url, "images/threads/", PhotoUrl);

        public string ThreadUrl { get; set; } = default!;

        public string TaleTitle { get; set; } = default!;

        public string TaleUrl { get; set; } = default!;

        public bool IsOnline { get; set; }

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");

        public List<ThreadAddendumSummary>? Addendums { get; set; }

    }
}
