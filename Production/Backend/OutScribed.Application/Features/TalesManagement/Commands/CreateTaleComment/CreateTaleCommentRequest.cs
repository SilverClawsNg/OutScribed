namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTaleComment
{
    public class CreateTaleCommentRequest
    {
        public Guid TaleId { get; set; }

        public string Details { get; set; } = default!;

    }
}
