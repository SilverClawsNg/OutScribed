using System.Web;

namespace OutScribed.Client.Models
{
    public class ThreadAddendumSummary
    {
        public string Details { get; set; } = default!;

        public string DetailsDecoded => HttpUtility.HtmlDecode(Details);

        public DateTime Date { get; set; }

        public string DateToString => Date.ToString("dddd, dd MMMM yyyy");

    }
}
