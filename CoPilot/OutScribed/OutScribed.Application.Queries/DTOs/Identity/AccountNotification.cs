using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountNotification
    {

        public int Id { get; set; }

        public Ulid NotificationId { get; set; }

        public Ulid AccountId { get; set; }

        public NotificationType Type { get; set; }

        public ContentType Content { get; set; }

        public DateTime Date { get; set; }

        public bool HasRead { get; set; }

        public string Text { get; set; } = default!;
    }
}
