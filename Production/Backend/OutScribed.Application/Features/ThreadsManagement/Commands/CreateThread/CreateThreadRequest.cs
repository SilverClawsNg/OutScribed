using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.CreateThread
{
    public class CreateThreadRequest
    {

        public Guid TaleId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }

    }
}
