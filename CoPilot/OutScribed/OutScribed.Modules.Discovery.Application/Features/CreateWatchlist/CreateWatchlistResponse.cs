using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Discovery.Application.Features.CreateWatchlist
{
    public class CreateWatchlistResponse(Guid Id, DateTime CreatedAt, string Summary,
        Country Country, Category Category, string SourceUrl, string SourceText)
    {

        public Ulid Id { get; set; } = Id;

        public DateTime CreatedAt { get; set; } = CreatedAt;

        public string Summary { get; set; } = Summary;

        public string SourceText { get; set; } = SourceText;

        public string SourceUrl { get; set; } = SourceUrl;

        public Category Category { get; set; } = Category;

        public Country Country { get; set; } = Country;
    }
}
