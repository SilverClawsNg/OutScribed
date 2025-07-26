using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadDetails
{
    public class UpdateThreadDetailsCommand : IRequest<Result<UpdateThreadDetailsResponse>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = null!;

    }
}
