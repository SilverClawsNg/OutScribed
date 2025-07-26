using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.FollowTale
{
    public class FollowTaleCommand : IRequest<Result<FollowTaleResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid AccountId { get; set; }

        public bool Option { get; set; }

    }
}
