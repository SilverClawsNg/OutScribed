using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class SendOTPEmailRequest
    {
        [Required(ErrorMessage = "Email address is required.")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Enter a valid email address")]
        [StringLength(56, ErrorMessage = "Email address must be no more than 56 characters.")]
        public string EmailAddress { get; set; } = null!;
    }
}
