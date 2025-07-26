using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleCountry
{
    public class UpdateTaleCountryRequest
    {

        public Guid Id { get; set; }

        public Countries? Country { get; set; }

    }
}
