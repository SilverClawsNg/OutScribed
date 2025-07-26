using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadShares
{
    public class LoadSharesResponse
    {
        public Ulid InsightId { get; set; }

        public SortType? Sort { get; set; }

        public ContactType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<InsightShare>? Shares { get; set; }

    }
}
