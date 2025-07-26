using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdateUserContactRequest
    {

        [Required(ErrorMessage = "Value is required")]
        [MaxLength(56, ErrorMessage = "Summary must not be more than 56 characters")]
        public string ContactValue { get; set; } = null!;

        [Required(ErrorMessage = "Type is required")]
        [EnumDataType(typeof(ContactTypes), ErrorMessage = "Select a valid type")]
        public ContactTypes? ContactType { get; set; }
    }
}
