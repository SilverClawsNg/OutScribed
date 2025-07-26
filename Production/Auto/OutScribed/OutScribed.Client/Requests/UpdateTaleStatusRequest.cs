using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdateTaleStatusRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [EnumDataType(typeof(TaleStatuses), ErrorMessage = "Select a valid status")]
        public TaleStatuses? Status { get; set; }

        public string? Reasons { get; set; }

    }
}
