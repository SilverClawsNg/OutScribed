using OutScribed.Client.Enums;

namespace OutScribed.Client.Models
{
    public class Contacts
    {
        public ContactTypes Type { get; set; }

        public string Text { get; set; } = default!;
    }
}
