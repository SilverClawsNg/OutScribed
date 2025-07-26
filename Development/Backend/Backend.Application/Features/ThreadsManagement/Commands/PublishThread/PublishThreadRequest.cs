using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Commands.PublishThread
{
    public class PublishThreadRequest
    {

        public Guid Id { get; set; }

        public bool Confirm { get; set; }

    }
}
