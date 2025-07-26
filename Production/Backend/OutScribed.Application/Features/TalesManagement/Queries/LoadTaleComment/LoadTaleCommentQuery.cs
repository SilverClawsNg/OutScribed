using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTaleComment
{
    public record LoadTaleCommentQuery(Guid AccountId, Guid CommentId)
        : IRequest<LoadTaleCommentQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid CommentId { get; set; } = CommentId;


    }
}
