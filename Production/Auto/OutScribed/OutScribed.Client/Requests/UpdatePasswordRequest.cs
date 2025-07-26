using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdatePasswordRequest
    {
        [Required(ErrorMessage = "Old password is required.")]
        [MinLength(8, ErrorMessage = "Old password must be at least 8 characters")]
        public string OldPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters")]
        public string Password { get; set; } = null!;
    }
}
