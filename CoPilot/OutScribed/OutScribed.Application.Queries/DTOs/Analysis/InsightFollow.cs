namespace OutScribed.Application.Queries.DTOs.Analysis
{
    public class InsightFollow
    {
        public int Id { get; set; }

        public Ulid InsightId { get; set; }

        public Ulid FollowId { get; set; }

        public Ulid AccountId { get; set; }

        public string Username { get; set; } = default!;

        public string UserPhoto { get; set; } = default!;

        public DateTime FollowedAt { get; set; }

    }
}
