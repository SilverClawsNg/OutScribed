using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTale
{
    public class UpdateTaleRequest
    {
        public Ulid Id { get; set; }

        public string? Title { get; set; }

        public Category? Category { get; set; }
    }
}
