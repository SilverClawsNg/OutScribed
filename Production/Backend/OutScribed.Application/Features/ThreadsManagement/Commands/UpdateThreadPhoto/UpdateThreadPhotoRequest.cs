namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadPhoto
{
    public class UpdateThreadPhotoRequest
    {

        public Guid Id { get; set; }

        public string Base64String { get; set; } = null!;

    }
}
