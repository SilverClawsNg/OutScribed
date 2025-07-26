using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadSummary
{
    public class UpdateThreadSummaryRequest
    {

        public Guid Id { get; set; }

        public string Summary { get; set; } = null!;

    }
}
