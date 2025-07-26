namespace OutScribed.Client.Requests
{
    public class CreateThreadResponseRequest
    {
        public Guid ThreadId { get; set; }

        public Guid CommentatorId { get; set; }

        public Guid ParentId { get; set; }

        public string Details { get; set; } = default!;
    }
}
