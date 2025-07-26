using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleStatus
{
    public class UpdateTaleStatusCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public TaleStatuses Status { get; set; }

        public string? Reasons { get; set; }

    }
}
