namespace OutScribed.Client.Requests
{
    public class CreateTaleCommentRequest
    {
        public Guid TaleId { get; set; }

        public string Details { get; set; } = default!;
    }
}
