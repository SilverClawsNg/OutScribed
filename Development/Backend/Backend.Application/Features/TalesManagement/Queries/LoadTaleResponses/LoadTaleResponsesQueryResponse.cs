using Backend.Application.Features.TalesManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleResponses
{

    public class LoadTaleResponsesQueryResponse
    {

        public Guid ParentId { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public bool More { get; set; }

        public int Counter { get; set; }

        public SortTypes Sort { get; set; }

        public List<TaleCommentSummary>? Responses { get; set; }

    }

}