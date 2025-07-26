using OutScribed.Domain.Enums;
using MediatR;
using OutScribed.Domain.Exceptions;

namespace OutScribed.Application.Features.TalesManagement.Commands.RateTaleComment
{
    public class RateTaleCommentCommand : IRequest<Result<RateTaleCommentResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid AccountId { get; set; }

        public Guid CommentId { get; set; }

        public RateTypes? RateType { get; set; }

    }
}
