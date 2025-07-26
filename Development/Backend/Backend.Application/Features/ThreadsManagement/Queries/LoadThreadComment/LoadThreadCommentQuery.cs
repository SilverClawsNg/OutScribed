using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Queries.LoadThreadComment
{
    public record LoadThreadCommentQuery(Guid AccountId, Guid CommentId)
        : IRequest<LoadThreadCommentQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid CommentId { get; set; } = CommentId;


    }
}
