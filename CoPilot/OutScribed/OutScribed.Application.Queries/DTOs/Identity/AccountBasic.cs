namespace OutScribed.Application.Queries.DTOs.Identity
{
    public class AccountBasic
    {
        public int Id { get; set; }

        public Ulid AccountId { get; set; }

        public string Username { get; set; } = default!;

        public string UserPhoto { get; set; } = default!;
    }
}
