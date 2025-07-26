using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleBasic
{
    public class UpdateTaleBasicCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Title { get; set; } = null!;

        public Categories? Category { get; set; }
    }
}
