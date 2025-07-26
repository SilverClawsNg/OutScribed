using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class FlagThreadRequest
    {

        public Guid ThreadId { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [EnumDataType(typeof(FlagTypes), ErrorMessage = "Select a valid reason")]
        public FlagTypes? FlagType { get; set; }

    }
}
