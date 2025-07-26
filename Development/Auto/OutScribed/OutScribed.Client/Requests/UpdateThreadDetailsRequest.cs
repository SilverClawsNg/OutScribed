using OutScribed.Client.Enums;

namespace OutScribed.Client.Requests
{
    public class UpdateThreadDetailsRequest
    {

        public Guid Id { get; set; }

        public string Details { get; set; } = null!;

    }
}
