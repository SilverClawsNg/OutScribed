using Backend.Application.Features.TalesManagement.Common;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleComment
{

    public class LoadTaleCommentQueryResponse
    {

        public TaleHeaderSummary? Tale { get; set; }

        public TaleCommentSummary? FocusComment { get; set; }

        public List<TaleCommentSummary>? RecursiveComments { get; set; }

    }

}