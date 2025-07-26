namespace OutScribed.Client.Requests
{
    public class CreateTaleResponseRequest
    {
        public Guid TaleId { get; set; }

        public Guid CommentatorId { get; set; }

        public Guid ParentId { get; set; }

        public string Details { get; set; } = default!;
    }
}
