using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdateThreadTagsRequest
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Tags is required")]
        [MaxLength(160, ErrorMessage = "You can only add five (5) tags with not more than 32 characters each")]
        public string Tags { get; set; } = null!;

    }
}
