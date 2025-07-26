using OutScribed.Application.Queries.DTOs.Identity;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Identity.LoadNotifications
{
    public class LoadNotificationsResponse()
    {
        public ContentType? Content { get; set; }

        public bool? HasRead { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<AccountNotification>? Notifications { get; set; }

    }
}
