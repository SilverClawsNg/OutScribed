using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleCommand : IRequest<Result<CreateTaleResponse?>>
    {

        public Guid AdminId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }
    }
}
