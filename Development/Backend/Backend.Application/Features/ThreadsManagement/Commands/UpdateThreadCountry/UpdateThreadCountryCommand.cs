using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadCountry
{
    public class UpdateThreadCountryCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public Countries? Country { get; set; }


    }
}
