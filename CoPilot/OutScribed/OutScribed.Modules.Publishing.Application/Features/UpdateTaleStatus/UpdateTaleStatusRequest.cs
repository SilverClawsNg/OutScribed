using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleStatus
{
    public class UpdateTaleStatusRequest
    {
        public Ulid? Id { get; set; }

        public TaleStatus? Status { get; set; }

        public string? Notes { get; set; }

    }
}
