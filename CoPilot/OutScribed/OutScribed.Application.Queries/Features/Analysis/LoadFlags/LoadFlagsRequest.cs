using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadFlags
{
    public class LoadFlagsRequest
    {
        public Ulid InsightId { get; set; }

        public SortType? Sort { get; set; }

        public FlagType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
