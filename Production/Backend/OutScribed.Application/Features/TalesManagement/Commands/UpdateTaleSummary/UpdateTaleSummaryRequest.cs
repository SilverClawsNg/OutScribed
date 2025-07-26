using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleSummary
{
    public class UpdateTaleSummaryRequest
    {

        public Guid Id { get; set; }

        public string Summary { get; set; } = null!;

    }
}
