using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class ThreadCommentResponse
    {
        public ThreadHeaderSummary? Thread { get; set; }

        public ThreadCommentSummary? FocusComment { get; set; }

        public List<ThreadCommentSummary>? RecursiveComments { get; set; }
    }
}
