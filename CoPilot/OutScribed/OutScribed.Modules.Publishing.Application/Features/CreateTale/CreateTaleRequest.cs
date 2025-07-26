using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.CreateTale
{
    public class CreateTaleRequest
    {
        public string? Title { get; set; }

        public Category? Category { get; set; }
    }
}
