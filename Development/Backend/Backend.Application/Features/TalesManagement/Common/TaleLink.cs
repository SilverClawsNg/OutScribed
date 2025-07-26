using System.Text.Json.Serialization;

namespace Backend.Application.Features.TalesManagement.Common
{
    public class TaleLink
    {
        public string Title { get; set; } = default!;

        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime Date { get; set; }

    }
}
