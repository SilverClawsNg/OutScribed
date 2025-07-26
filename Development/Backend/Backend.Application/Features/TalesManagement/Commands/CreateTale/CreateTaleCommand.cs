using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleCommand : IRequest<Result<CreateTaleResponse?>>
    {

        public Guid AdminId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }
    }
}
