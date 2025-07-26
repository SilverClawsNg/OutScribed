using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleBasic
{
    public class UpdateTaleBasicCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }
    }
}
