using Backend.Domain.Abstracts;
using Backend.Domain.Enums;

namespace Backend.Domain.Events
{
    public record ActivityUpdatedEvent
    (DateTime Date, Guid ActorId, string Details, ActivityTypes Type, 
        ActivityConstructorTypes ConstructorType)
    : IDomainEvent
    {
        public Guid ActorId { get; set; } = ActorId;

        public DateTime Date { get; set; } = Date;

        public string Details { get; set; } = Details;

        public ActivityTypes Type { get; set; } = Type;

        public ActivityConstructorTypes ConstructorType { get; set; } = ConstructorType;


    }

}
