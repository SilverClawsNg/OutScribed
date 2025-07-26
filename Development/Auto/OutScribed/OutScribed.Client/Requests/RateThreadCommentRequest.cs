using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class RateThreadCommentRequest
    {
        public Guid ThreadId { get; set; }

        public Guid CommentId { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [EnumDataType(typeof(RateTypes), ErrorMessage = "Select a valid type")]
        public RateTypes RateType { get; set; }
    }
}
