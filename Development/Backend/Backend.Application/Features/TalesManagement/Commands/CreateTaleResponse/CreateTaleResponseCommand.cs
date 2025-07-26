using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.CreateTaleResponse
{
    public class CreateTaleResponseCommand : IRequest<Result<CreateTaleResponseResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid CommentatorId { get; set; }

        public Guid ParentId { get; set; }

        public Guid AccountId { get; set; }

        public string Details { get; set; } = default!;


    }
}
