using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class CreateWatchListRequest
    {

        [Required(ErrorMessage = "Title is required")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Title must be between 8 and 128 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Summary is required")]
        [StringLength(1024, MinimumLength = 30, ErrorMessage = "Summary must be between 30 and 1024 characters")]
        public string Summary { get; set; } = null!;

        [Required(ErrorMessage = "Source url is required")]
        [StringLength(128, ErrorMessage = "Source url not exceed 128 characters")]
        public string SourceUrl { get; set; } = null!;

        [Required(ErrorMessage = "Source text is required")]
        [StringLength(28, ErrorMessage = "Source text not exceed 28 characters")]
        public string SourceText { get; set; } = null!;

        [Required(ErrorMessage = "Category is required")]
        [EnumDataType(typeof(Categories), ErrorMessage = "Select a valid category")]
        public Categories? Category { get; set; }

        [EnumDataType(typeof(Countries), ErrorMessage = "Select a valid country")]
        public Countries? Country { get; set; }

    }
}
