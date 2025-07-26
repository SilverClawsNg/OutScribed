using OutScribed.SharedKernel.Abstract;

namespace OutScribed.Modules.Onboarding.Domain.Events
{
 
    public class TokenSentEvent(Ulid tempUserId, string emailAddress, string token) 
        : DomainEvent
    {
        public Ulid TempUserId { get; set; } = tempUserId;

        public string EmailAddress { get; set; } = emailAddress;

        public string Token { get; set; } = token;
    }
}
