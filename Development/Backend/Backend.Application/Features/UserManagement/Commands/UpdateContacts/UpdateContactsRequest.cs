using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.UpdateContacts
{
    public class UpdateContactsRequest
    {
        public string ContactValue { get; set; } = null!;

        public ContactTypes ContactType { get; set; }

    }

}
