using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Commands.SubmitWriterApplication
{
    public class SubmitWriterApplicationRequest
    {

        public Countries? Country { get; set; }

        public string Address { get; set; } = null!;

        public string Base64String { get; set; } = null!;

    }
}
