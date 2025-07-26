using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.CreateTaleComment
{
    public class CreateTaleCommentCommand : IRequest<Result<CreateTaleCommentResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = default!;

    }
}
