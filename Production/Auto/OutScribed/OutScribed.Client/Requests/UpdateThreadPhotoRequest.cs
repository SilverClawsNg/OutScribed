namespace OutScribed.Client.Requests
{
    public class UpdateThreadPhotoRequest
    {

        public Guid Id { get; set; }

        public string Base64String { get; set; } = null!;

    }
}
