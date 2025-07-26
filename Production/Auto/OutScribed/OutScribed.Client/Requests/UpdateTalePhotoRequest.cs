namespace OutScribed.Client.Requests
{
    public class UpdateTalePhotoRequest
    {

        public Guid Id { get; set; }

        public string Base64String { get; set; } = null!;

    }
}
