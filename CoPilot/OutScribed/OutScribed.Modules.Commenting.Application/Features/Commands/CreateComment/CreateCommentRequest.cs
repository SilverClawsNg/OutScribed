using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Commenting.Application.Features.Commands.CreateComment
{
    public class CreateCommentRequest
    {
        public Guid? ContentId { get; set; }

        public ContentType ContentType { get; set; }

        public string? Details { get; set; }
    }
}
