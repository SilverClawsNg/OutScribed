using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.UserManagement.Commands.UpdateContacts
{
    public class UpdateContactsRequest
    {
        public string ContactValue { get; set; } = null!;

        public ContactTypes ContactType { get; set; }

    }

}
