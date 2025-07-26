namespace OutScribed.Client.Requests
{
    public class UpdateTaleDetailsRequest
    {

        public Guid Id { get; set; }

        public string Details { get; set; } = null!;

    }
}
