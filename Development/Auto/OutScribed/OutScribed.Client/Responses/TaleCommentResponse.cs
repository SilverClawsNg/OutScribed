using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class TaleCommentResponse
    {
        public TaleHeaderSummary? Tale { get; set; }

        public TaleCommentSummary? FocusComment { get; set; }

        public List<TaleCommentSummary>? RecursiveComments { get; set; }
    }
}
