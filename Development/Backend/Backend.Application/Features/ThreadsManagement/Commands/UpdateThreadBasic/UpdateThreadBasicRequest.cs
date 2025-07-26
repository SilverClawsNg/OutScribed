using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadBasic
{
    public class UpdateThreadBasicRequest
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }

    }
}
