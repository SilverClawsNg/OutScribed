using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Flagging.Application.Features.Commands.FlagContent
{
    public class FlagContentRequest
    {
        public Guid? ContentId { get; set; }

        public ContentType Content { get; set; }

        public FlagType? Type { get; set; }

    }
}
