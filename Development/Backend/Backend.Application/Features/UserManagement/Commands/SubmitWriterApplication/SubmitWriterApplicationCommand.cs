using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.SubmitWriterApplication
{
    public class SubmitWriterApplicationCommand : IRequest<Result<bool>>
    {

        public Guid AccountId { get; set; }

        public Countries? Country { get; set; }

        public string Address { get; set; } = null!;

        public string Base64String { get; set; } = null!;
    }
}
