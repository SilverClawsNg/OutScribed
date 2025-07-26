using OutScribed.Client.Enums;

namespace OutScribed.Client.Models
{
    public class TaleHistorySummary
    {
        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

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

        public Guid AdminId { get; set; }

        public string AdminUsername { get; set; } = default!;

        public string? Reasons { get; set; }

    }
}
