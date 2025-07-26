using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdateProfileRequest
    {

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(128, ErrorMessage = "Title must not be more than 128 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Bio is required.")]
        [MaxLength(512, ErrorMessage = "Bio must not be more than 512 characters.")]
        public string? Bio { get; set; } = null!;

        public string Base64String { get; set; } = null!;

        [StringLength(24, ErrorMessage = "Phone number must be no more than 24 characters.")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address")]
        [StringLength(56, ErrorMessage = "Email address must be no more than 56 characters.")]
        public string? EmailAddress { get; set; }

        public bool IsHidden { get; set; }

    }
}
