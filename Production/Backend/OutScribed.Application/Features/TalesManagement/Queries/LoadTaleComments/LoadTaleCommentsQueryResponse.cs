using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTaleComments
{

    public class LoadTaleCommentsQueryResponse
    {

        public Guid TaleId { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public bool More { get; set; }

        public int Counter { get; set; }

        public SortTypes Sort { get; set; }

        public List<TaleCommentSummary>? Comments { get; set; }

    }

}