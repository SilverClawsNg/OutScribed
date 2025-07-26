using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.UpdateTaleCountry
{
    public class UpdateTaleCountryRequest
    {
        public Ulid? Id { get; set; }

        public Country? Country { get; set; }
    }
}
