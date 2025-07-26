using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadSummary
{
    public class UpdateThreadSummaryRequest
    {

        public Guid Id { get; set; }

        public string Summary { get; set; } = null!;

    }
}
