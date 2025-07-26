using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.FlagInsight
{
    public class FlagInsightRequest
    {
        public Ulid? InsightId { get; set; }

        public FlagType? Type { get; set; }

    }
}
