using Backend.Domain.Enums;
using System.Text.Json.Serialization;

namespace Backend.Application.Features.UserManagement.Common
{
    public class ActivitySummary
    {

        public ActivityConstructorTypes ConstructorType { get; set; }

        public ActivityTypes Type { get; set; }

        public DateTime Date { get; set; }

        [JsonIgnore]
        public Guid Id { get; set; }

        public bool HasRead { get; set; }
       
        public string Details { get; set; } = default!;

    }
}
