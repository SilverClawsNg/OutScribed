using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class SendOTPPhoneRequest
    {
        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(24, ErrorMessage = "Phone number must be no more than 24 characters.")]
        public string PhoneNumber { get; set; } = null!;
    }
}
