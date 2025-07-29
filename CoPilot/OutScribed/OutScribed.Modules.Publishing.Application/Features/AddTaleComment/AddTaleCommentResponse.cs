using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.AddTaleComment
{
    public class AddTaleCommentResponse()
    {

        //public Ulid TaleId { get; set; }

        public DateTime CommentedAt { get; set; }

        public required string Text { get; set; }

        //public int Responses { get; set; }

        //public int Upvotes { get; set; }

        //public int Downvotes { get; set; }

        //public RatingType MyRating { get; set; }

        //public required string CommentatorPhotoUrl { get; set; }

        //public required string CommentatorUsername { get; set; }

        //public Ulid CommentatorId { get; set; }
    }
}
