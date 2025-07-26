using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class SendOTPUsernameRequest
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(20, ErrorMessage = "Username must be no more than 20 characters.")]
        public string Username { get; set; } = null!;
    }
}
