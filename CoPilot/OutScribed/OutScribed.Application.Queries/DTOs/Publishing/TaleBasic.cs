namespace OutScribed.Application.Queries.DTOs.Publishing
{
    public class TaleBasic
    {
        public Ulid Id { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public string Title { get; set; } = default!;
    }
}
