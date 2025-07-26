using OutScribed.Application.Features.ThreadsManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadComments
{

    public class LoadThreadCommentsQueryResponse
    {

        public Guid ThreadId { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public bool More { get; set; }

        public int Counter { get; set; }

        public SortTypes Sort { get; set; }

        public List<ThreadCommentSummary>? Comments { get; set; }

    }

}