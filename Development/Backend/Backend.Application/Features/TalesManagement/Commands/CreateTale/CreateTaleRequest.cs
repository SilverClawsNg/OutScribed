using Backend.Domain.Enums;

namespace Backend.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleRequest
    {

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }

    }
}
