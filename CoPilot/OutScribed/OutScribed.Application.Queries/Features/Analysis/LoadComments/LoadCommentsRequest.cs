using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadComments
{
    public class LoadCommentsRequest
    {
        public Ulid InsightId { get; set; }

        public SortType? Sort { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
