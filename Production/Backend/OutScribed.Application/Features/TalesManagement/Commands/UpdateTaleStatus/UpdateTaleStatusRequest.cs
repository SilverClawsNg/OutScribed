using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleStatus
{
    public class UpdateTaleStatusRequest
    {

        public Guid Id { get; set; }

        public TaleStatuses Status { get; set; }

        public string? Reasons { get; set; }

    }
}
