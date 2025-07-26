namespace Backend.Application.Features.ThreadsManagement.Commands.CreateThreadComment
{
    public class CreateThreadCommentRequest
    {
        public Guid ThreadId { get; set; }

        public string Details { get; set; } = default!;

    }
}
