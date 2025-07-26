using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateContact
{
    public class UpdateContactRequest
    {
        public string? Title { get; set; }

        public ContactType ContactType { get; set; }
    }
}
