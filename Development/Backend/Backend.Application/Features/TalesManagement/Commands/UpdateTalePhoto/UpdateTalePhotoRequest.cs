namespace Backend.Application.Features.TalesManagement.Commands.UpdateTalePhoto
{
    public class UpdateTalePhotoRequest
    {

        public Guid Id { get; set; }

        public string Base64String { get; set; } = null!;

    }
}
