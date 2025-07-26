namespace OutScribed.Client.Requests
{
    public class CreateThreadCommentRequest
    {
        public Guid ThreadId { get; set; }

        public string Details { get; set; } = default!;
    }
}
