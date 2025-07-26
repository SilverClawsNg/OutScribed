namespace OutScribed.Application.Queries.DTOs.Analysis
{
    public class InsightAddendum
    {

        public int Id { get; set; }

        public Ulid InsightId { get; set; }

        public Ulid AddendumId { get; set; }

        public string Text { get; set; } = default!;

        public DateTime Date { get; set; }

    }
}
