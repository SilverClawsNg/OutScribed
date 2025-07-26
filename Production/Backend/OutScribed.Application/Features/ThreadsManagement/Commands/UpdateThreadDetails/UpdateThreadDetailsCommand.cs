using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadDetails
{
    public class UpdateThreadDetailsCommand : IRequest<Result<UpdateThreadDetailsResponse>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = null!;

    }
}
