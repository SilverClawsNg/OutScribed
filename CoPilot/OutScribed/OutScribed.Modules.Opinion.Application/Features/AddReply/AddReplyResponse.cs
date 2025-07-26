namespace OutScribed.Modules.Analysis.Application.Features.AddReply
{
    public class AddReplyResponse()
    {

        public Ulid InsightId { get; set; }

        public Ulid CommentId { get; set; }

        public DateTime CommentedAt { get; set; }

        public string Text { get; set; } = default!;


        //Already exists on frontend so may not be necessary to resend
        //public string UserPhoto { get; set; } = default!;

        //public string Username { get; set; } = default!;

        //public Ulid AccountId { get; set; }

    }
}
