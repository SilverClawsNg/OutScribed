using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadCountry
{
    public class UpdateThreadCountryRequest
    {

        public Guid Id { get; set; }

        public Countries? Country { get; set; }

    }
}
