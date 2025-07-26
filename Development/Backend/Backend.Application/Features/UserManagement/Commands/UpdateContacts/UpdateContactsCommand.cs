using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.UserManagement.Commands.UpdateContacts
{
    public class UpdateContactsCommand : IRequest<Result<bool>>
    {
        public Guid AccountId { get; set; }

        public string ContactValue { get; set; } = null!;

        public ContactTypes ContactType { get; set; }

    }

}
