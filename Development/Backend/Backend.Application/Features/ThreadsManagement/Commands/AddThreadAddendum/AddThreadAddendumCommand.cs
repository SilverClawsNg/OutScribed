using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.AddThreadAddendum
{
    public class AddThreadAddendumCommand : IRequest<Result<AddThreadAddendumResponse>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = null!;

    }
}
