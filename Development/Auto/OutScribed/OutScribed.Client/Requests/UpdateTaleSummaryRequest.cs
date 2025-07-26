using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdateTaleSummaryRequest
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Summary is required")]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Summary must be between 8 and 256 characters")]
        public string Summary { get; set; } = null!;

    }
}
