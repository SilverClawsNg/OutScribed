using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Common
{
    public class Contacts
    {

        public ContactTypes Type { get; set; }

        public string Text { get; set; } = default!;
    }
}
