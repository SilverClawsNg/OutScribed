namespace OutScribed.Modules.Publishing.Application.Features.UpdateTalePhoto
{
    public class UpdateTalePhotoResponse(string PhotoUrl)
    {
        public string PhotoUrl { get; set; } = PhotoUrl;

    }
}
