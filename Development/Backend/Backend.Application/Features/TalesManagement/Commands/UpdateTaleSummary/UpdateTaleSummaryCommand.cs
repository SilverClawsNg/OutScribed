using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleSummary
{
    public class UpdateTaleSummaryCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Summary { get; set; } = null!;

    }
}
