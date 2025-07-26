namespace OutScribed.Modules.Publishing.Application.Features.UpdateTalePhoto
{
    public class UpdateTalePhotoRequest
    {
        public Ulid? Id { get; set; }

        public string? Base64String { get; set; }
    }
}
