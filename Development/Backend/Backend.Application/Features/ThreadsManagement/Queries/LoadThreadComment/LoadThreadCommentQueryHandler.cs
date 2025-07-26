using Backend.Application.Features.ThreadsManagement.Common;
using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Queries.LoadThreadComment
{
    public class LoadThreadCommentQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadThreadCommentQuery, LoadThreadCommentQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadThreadCommentQueryResponse> Handle(LoadThreadCommentQuery request, CancellationToken cancellationToken)
        {

            var focusComment = await _unitOfWork.ThreadsRepository.LoadThreadComment(request.AccountId, request.CommentId);

            if(focusComment == null)
                return new LoadThreadCommentQueryResponse();

            var thread = await _unitOfWork.ThreadsRepository.LoadThreadHeaderSummary(request.AccountId, focusComment.ThreadId);

            List<ThreadCommentSummary> recursiveComments = [];

            Guid? parentId = focusComment?.ParentId;

            while (parentId != null)
            {

                var parentComment = await _unitOfWork.ThreadsRepository.LoadThreadComment(request.AccountId, parentId.Value);

                if(parentComment != null)
                {
                    recursiveComments.Add(parentComment);

                    parentId = parentComment.ParentId;
                }
                else
                {
                    parentId = null;
                }
            }


            return new LoadThreadCommentQueryResponse()
            {
                Thread = thread,
                FocusComment = focusComment,
                RecursiveComments = recursiveComments

            };
        }
    }

}
