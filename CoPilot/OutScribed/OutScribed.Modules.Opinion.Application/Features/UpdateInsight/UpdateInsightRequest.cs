using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsight
{
    public class UpdateInsightRequest
    {
        public Guid? Id { get; set; }

        public string? Title { get; set; }

        public Category? Category { get; set; }
    }
}
