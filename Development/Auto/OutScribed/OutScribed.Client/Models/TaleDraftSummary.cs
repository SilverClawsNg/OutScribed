using OutScribed.Client.Enums;
using OutScribed.Client.Globals;
using System.Web;

namespace OutScribed.Client.Models
{
    public class TaleDraftSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public Guid WriterId { get; set; }

        public string WriterUsername { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? TaleUrl { get; set; }

        public string? Summary { get; set; }

        public List<string>? Tags { get; set; }

        public string? TagsToString => Tags == null ? null : string.Join(", ", Tags);
        
        public string? PhotoUrl { get; set; }

        public string? PhotoUrlToString => PhotoUrl == null ? null : string.Format("{0}{1}{2}", Url, "images/tales/", PhotoUrl);

        public string? Details { get; set; }

        public string? DetailsDecoded => Details == null ? null : HttpUtility.HtmlDecode(Details);

        public TaleStatuses Status { get; set; }

        public string StatusToString => 
            Status == TaleStatuses.Created ? "Under Creation"
            : Status == TaleStatuses.Submitted ? "Submitted For Review"
            : Status == TaleStatuses.Checked ? "Passed Legal Vetting"
            : Status == TaleStatuses.Edited ? "Passed Story Relevance Vetting"
            : Status == TaleStatuses.Published ? "Passed Publication Vetting"
             : Status == TaleStatuses.UnChecked ? "Returned For Review (Legal Vetting)"
            : Status == TaleStatuses.UnEdited ? "Returned For Review (Story Relevance Vetting)"
            : Status == TaleStatuses.UnPublished ? "Returned For Review (Publication Vetting)"
             : Status == TaleStatuses.ReChecked ? "Resubmitted (Legal Vetting)"
            : Status == TaleStatuses.ReEdited ? "Resubmitted (Story Relevance Vetting)"
            : Status == TaleStatuses.RePublished ? "Resubmitted (Publication Vetting)"
              : Status == TaleStatuses.OutChecked ? "Rejected (Legal Vetting)"
            : Status == TaleStatuses.OutEdited ? "Rejected (Story Relevance Vetting)"
            : Status == TaleStatuses.OutPublished ? "Rejected (Publication Vetting)"
            : "Error";

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

        public Countries? Country { get; set; }

        public string? CountryToString => Country?.ToString().Replace("_", " ");

        public Categories Category { get; set; }

        public string CategoryToString => Category.ToString().Replace("8", "&").Replace("_", " ");

        public List<TaleHistorySummary> History { get; set; } = default!;

    }
}
