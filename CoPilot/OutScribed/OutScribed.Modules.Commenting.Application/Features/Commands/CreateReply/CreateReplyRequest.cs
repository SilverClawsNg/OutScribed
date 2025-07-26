namespace OutScribed.Modules.Commenting.Application.Features.Commands.CreateReply
{
    public class CreateReplyRequest
    {
        public Guid? CommentId { get; set; }

        public string? Details { get; set; }
    }
}
