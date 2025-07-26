using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class FlagThreadCommentRequest
    {

        public Guid ThreadId { get; set; }

        public Guid CommentId { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [EnumDataType(typeof(FlagTypes), ErrorMessage = "Select a valid reason")]
        public FlagTypes? FlagType { get; set; }

    }
}
