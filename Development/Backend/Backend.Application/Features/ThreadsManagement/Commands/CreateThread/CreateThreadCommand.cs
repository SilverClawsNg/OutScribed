using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThread
{
    public class CreateThreadCommand : IRequest<Result<bool>>
    {

        public Guid AccountId { get; set; }

        public Guid TaleId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }
    }
}
