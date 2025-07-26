using Backend.Domain.Enums;
using MediatR;
using Backend.Domain.Exceptions;

namespace Backend.Application.Features.TalesManagement.Commands.RateTaleComment
{
    public class RateTaleCommentCommand : IRequest<Result<RateTaleCommentResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CommentId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
