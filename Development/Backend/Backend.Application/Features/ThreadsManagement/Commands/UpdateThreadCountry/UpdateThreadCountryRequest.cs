using Backend.Domain.Enums;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadCountry
{
    public class UpdateThreadCountryRequest
    {

        public Guid Id { get; set; }

        public Countries? Country { get; set; }

    }
}
