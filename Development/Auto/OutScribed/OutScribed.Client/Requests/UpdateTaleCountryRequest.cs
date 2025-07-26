using OutScribed.Client.Enums;
using System.ComponentModel.DataAnnotations;

namespace OutScribed.Client.Requests
{
    public class UpdateTaleCountryRequest
    {

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [EnumDataType(typeof(Countries), ErrorMessage = "Select a valid country")]
        public Countries? Country { get; set; }
    }
}
