using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class CreateAccountPhoneRequest
    {

        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username must be no more than 20 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = null!;
    }
}
