using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Rating.Application.Features.Commands.RateContent
{
    public class RateContentRequest
    {
        public Guid? ContentId { get; set; }

        public ContentType Content { get; set; }

        public RatingType? Type { get; set; }
    }
}
