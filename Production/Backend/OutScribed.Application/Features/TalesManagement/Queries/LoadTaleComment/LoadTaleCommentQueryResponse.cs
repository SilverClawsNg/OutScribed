using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTaleComment
{

    public class LoadTaleCommentQueryResponse
    {

        public TaleHeaderSummary? Tale { get; set; }

        public TaleCommentSummary? FocusComment { get; set; }

        public List<TaleCommentSummary>? RecursiveComments { get; set; }

    }

}