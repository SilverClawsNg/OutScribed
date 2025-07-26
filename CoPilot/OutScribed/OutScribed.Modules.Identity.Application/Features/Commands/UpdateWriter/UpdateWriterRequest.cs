namespace OutScribed.Modules.Identity.Application.Features.Commands.UpdateWriter
{
    public class UpdateWriterRequest
    {
        public Ulid? AccountId { get; set; }

        public bool? IsActive { get; set; }
    }
}
