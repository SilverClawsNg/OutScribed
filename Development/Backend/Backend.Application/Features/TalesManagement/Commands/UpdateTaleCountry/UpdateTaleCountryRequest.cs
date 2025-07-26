using Backend.Domain.Enums;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleCountry
{
    public class UpdateTaleCountryRequest
    {

        public Guid Id { get; set; }

        public Countries? Country { get; set; }

    }
}
