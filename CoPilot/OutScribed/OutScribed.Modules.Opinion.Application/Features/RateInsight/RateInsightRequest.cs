using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.RateInsight
{
    public class RateInsightRequest
    {
        public Ulid? InsightId { get; set; }

        public RatingType? Type { get; set; }
    }
}
