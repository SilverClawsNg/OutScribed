using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleCountry
{
    public class UpdateTaleCountryCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Countries? Country { get; set; }

        public Guid AdminId { get; set; }

    }
}
