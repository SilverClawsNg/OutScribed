using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.AddInsightComment
{
    public class AddInsightCommentResponse()
    {

        public Ulid InsightId { get; set; }

        public DateTime CommentedAt { get; set; }

        public string Text { get; set; } = default!;


        //public int RepliesCount { get; set; }

        //public int Upvotes { get; set; }

        //public int Downvotes { get; set; }

        //public RatingType? MyRating { get; set; }

        //public string UserPhoto { get; set; } = default!;

        //public string Username { get; set; } = default!;

        //public Ulid AccountId { get; set; }
    }
}
