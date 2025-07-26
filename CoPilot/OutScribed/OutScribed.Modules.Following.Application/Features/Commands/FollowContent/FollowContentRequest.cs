using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Following.Application.Features.Commands.FollowContent
{
    public class FollowContentRequest
    {
        public Guid? ContentId { get; set; }

        public ContentType Content { get; set; }

    }
}
