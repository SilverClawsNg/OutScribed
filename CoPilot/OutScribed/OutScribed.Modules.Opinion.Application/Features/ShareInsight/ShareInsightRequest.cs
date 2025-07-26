using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.ShareInsight
{
    public class ShareInsightRequest
    {
        public Guid? InsightId { get; set; }

        public string? Handle { get; set; }

        public ContactType? Contact { get; set; }
    }
}
