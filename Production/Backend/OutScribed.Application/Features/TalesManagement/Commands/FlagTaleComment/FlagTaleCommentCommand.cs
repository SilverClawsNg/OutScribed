using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.FlagTaleComment
{
    public class FlagTaleCommentCommand : IRequest<Result<FlagTaleCommentResponse>>
    {
        public Guid TaleId { get; set; }

        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }

        public FlagTypes? FlagType { get; set; }

    }
}
