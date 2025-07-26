using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdateContacts
{
    public class UpdateContactsCommand : IRequest<Result<bool>>
    {
        public Guid AccountId { get; set; }

        public string ContactValue { get; set; } = null!;

        public ContactTypes ContactType { get; set; }

    }

}
