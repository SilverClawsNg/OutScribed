using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadSummary
{
    public class UpdateThreadSummaryCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Summary { get; set; } = null!;

    }
}
