using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Following.Application.Features.Commands.UnfollowContent
{
    public class UnfollowContentRequest
    {
        public Guid? ContentId { get; set; }

        public ContentType Content { get; set; }

    }
}
