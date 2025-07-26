using OutScribed.Client.Enums;

namespace OutScribed.Client.Requests
{
    public class PublishThreadRequest
    {
        public Guid Id { get; set; }

        public bool Confirm { get; set; }

    }
}
