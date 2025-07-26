using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadFlags
{
    public class LoadFlagsResponse
    {
        public Ulid InsightId { get; set; }

        public SortType? Sort { get; set; }

        public FlagType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<InsightFlag>? Flags { get; set; }

    }
}
