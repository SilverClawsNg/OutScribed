using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.CreateInsight
{
    public class CreateInsightRequest
    {

        public Guid? TaleId { get; set; }

        public string? Title { get; set; }

        public Category? Category { get; set; }

    }
}
