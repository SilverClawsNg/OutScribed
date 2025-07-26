namespace OutScribed.Modules.Discovery.Application.Features.AddReply
{
    public class AddReplyResponse()
    {

        public Ulid ContentId { get; set; }

        public Ulid CommentId { get; set; }

        public DateTime CommentedAt { get; set; }

        public string Text { get; set; } = default!;
    }
}
