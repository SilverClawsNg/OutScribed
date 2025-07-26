using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadBasic
{
    public class UpdateThreadBasicCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }
    }
}
