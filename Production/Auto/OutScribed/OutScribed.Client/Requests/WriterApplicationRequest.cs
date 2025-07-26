using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class WriterApplicationRequest
    {
        [Required(ErrorMessage = "Country is required")]
        [EnumDataType(typeof(Countries), ErrorMessage = "Select a valid country")]
        public Countries? Country { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(128, ErrorMessage = "Address must not be more than 128 characters")]
        public string Address { get; set; } = null!;

        public string Base64String { get; set; } = null!;
    }
}
