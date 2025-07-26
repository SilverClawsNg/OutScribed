using OutScribed.Application.Features.ThreadsManagement.Common;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadComment
{

    public class LoadThreadCommentQueryResponse
    {

        public ThreadHeaderSummary? Thread { get; set; }

        public ThreadCommentSummary? FocusComment { get; set; }

        public List<ThreadCommentSummary>? RecursiveComments { get; set; }

    }

}