namespace OutScribed.Modules.Publishing.Application.Features.RemoveTaleComment
{
    public class RemoveTaleCommentRequest
    {
        public Ulid? TaleId { get; set; }

        public Ulid? CommentId { get; set; }

    }
}
