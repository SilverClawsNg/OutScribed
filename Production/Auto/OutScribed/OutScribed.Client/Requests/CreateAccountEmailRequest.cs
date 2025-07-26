using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class CreateAccountEmailRequest
    {

        public string EmailAddress { get; set; } = null!;

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(16, ErrorMessage = "Username must be no more than 16 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; } = null!;
    }
}
