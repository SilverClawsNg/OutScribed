namespace OutScribed.Application.Queries.DTOs.Publishing
{
    public class TaleFollow
    {
        public int Id { get; set; }

        public Ulid TaleId { get; set; }

        public Ulid FollowId { get; set; }

        public Ulid AccountId { get; set; }

        public string Username { get; set; } = default!;

        public string UserPhoto { get; set; } = default!;

        public DateTime FollowedAt { get; set; }

    }
}
