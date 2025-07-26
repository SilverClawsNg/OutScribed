using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Commenting.Application.Features.Commands.CreateComment
{
    public class CreateCommentResponse()
    {

        public Guid ContentId { get; set; }

        public DateTime CommentedAt { get; set; }

        public required string Details { get; set; }

        public int Responses { get; set; }

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public ReactionType MyReaction { get; set; }

        public required string CommentatorPhotoUrl { get; set; }

        public required string CommentatorUsername { get; set; }

        public Guid CommentatorId { get; set; }
    }
}
