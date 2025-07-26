using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Application.Features.Commands.ApplyAsWriter
{
    public class ApplyAsWriterRequest
    {
        public Country? Country { get; set; }

        public string? Address { get; set; }

        public string? Base64String { get; set; }
    }
}
