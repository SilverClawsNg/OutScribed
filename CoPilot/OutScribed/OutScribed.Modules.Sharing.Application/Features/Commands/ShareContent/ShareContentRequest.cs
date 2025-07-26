using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Sharing.Application.Features.Commands.ShareContent
{
    public class ShareContentRequest
    {
        public Guid? ContentId { get; set; }

        public ContentType Content { get; set; }

        public ContactType? Contact { get; set; }
    }
}
