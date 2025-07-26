using Backend.Application.Features.TalesManagement.Common;
using Backend.Application.Repositories;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleComment
{
    public class LoadTaleCommentQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<LoadTaleCommentQuery, LoadTaleCommentQueryResponse>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<LoadTaleCommentQueryResponse> Handle(LoadTaleCommentQuery request, CancellationToken cancellationToken)
        {

            var focusComment = await _unitOfWork.TaleRepository.LoadTaleComment(request.AccountId, request.CommentId);

            if(focusComment == null)
                return new LoadTaleCommentQueryResponse();

            var tale = await _unitOfWork.TaleRepository.LoadTaleHeaderSummary(request.AccountId, focusComment.TaleId);

            List<TaleCommentSummary> recursiveComments = [];

            Guid? parentId = focusComment?.ParentId;

            while (parentId != null)
            {

                var parentComment = await _unitOfWork.TaleRepository.LoadTaleComment(request.AccountId, parentId.Value);

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


            return new LoadTaleCommentQueryResponse()
            {
                Tale = tale,
                FocusComment = focusComment,
                RecursiveComments = recursiveComments

            };
        }
    }

}
