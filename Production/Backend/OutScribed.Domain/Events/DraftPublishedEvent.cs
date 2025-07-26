using OutScribed.Domain.Abstracts;

namespace OutScribed.Domain.Events
{
    public record DraftPublishedEvent(Guid DraftId, Guid AccountId, string Url)
     : IDomainEvent
    {
     
        public Guid DraftId { get; set; } = DraftId;

        public Guid AccountId { get; set; } = AccountId;

        public string Url { get; set; } = Url;

    }

}
