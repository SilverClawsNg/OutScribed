using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.FlagTale
{
    public class FlagTaleCommand : IRequest<Result<FlagTaleResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid UserId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
