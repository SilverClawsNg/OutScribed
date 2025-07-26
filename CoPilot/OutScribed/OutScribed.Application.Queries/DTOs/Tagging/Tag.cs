namespace OutScribed.Application.Queries.DTOs.Tagging
{
    public class Tag
    {

        public int Id { get; set; }

        public Ulid TagId { get; set; } 

        public string Name { get; set; } = default!;

        public string Slug { get; set; } = default!;

        public int InsightsCounter { get; set; }

        public int TalesCounter { get; set; }

        public int WatchlistsCounter { get; set; }

        public int TotalCounts { get; set; }

    }
}
