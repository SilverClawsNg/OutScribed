using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleBasic
{
    public class UpdateTaleBasicRequest
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }

    }
}
