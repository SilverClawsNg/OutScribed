using OutScribed.SharedKernel.Utilities;

namespace OutScribed.SharedKernel.Abstract
{
    public abstract class DomainEvent
    {
        public Ulid EventId { get; } = IdGenerator.Generate();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        protected DomainEvent() { }
    }

}
