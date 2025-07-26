using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleRequest
    {

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }

    }
}
