using OutScribed.Application.Features.TalesManagement.Commands.RateTale;
using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.RateTale
{
    public class RateTaleCommand : IRequest<Result<RateTaleResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid UserId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
