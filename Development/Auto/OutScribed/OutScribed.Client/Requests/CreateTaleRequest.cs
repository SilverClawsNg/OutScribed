using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class CreateTaleRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Title must be between 8 and 128 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Category is required")]
        [EnumDataType(typeof(Categories), ErrorMessage = "Select a valid category")]
        public Categories? Category { get; set; }

    }
}
