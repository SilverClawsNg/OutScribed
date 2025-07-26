using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Discovery.Application.Features.UpdateWatchlist
{
    public class UpdateWatchlistRequest
    {

        public Ulid? Id { get; set; }

        public string? Summary { get; set; }

        public string? SourceText { get; set; }

        public string? SourceUrl { get; set; }

        public Category? Category { get; set; }

        public Country? Country { get; set; }
    }
}
