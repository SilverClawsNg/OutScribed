namespace OutScribed.Modules.Publishing.Application.Features.AddTaleComment
{
    public class AddTaleCommentRequest
    {
        public Ulid? TaleId { get; set; }

        public string? Text { get; set; }
    }
}
