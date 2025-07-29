namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleComment
{
    public class UpdateTaleCommentRequest
    {
        public Ulid? TaleId { get; set; }

        public Ulid? CommentId { get; set; }

        public string? Text { get; set; }
    }
}
