using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Discovery.Application.Features.CreateWatchlist
{
    public class CreateWatchlistRequest
    {

        public string? Summary { get; set; }

        public string? SourceText { get; set; }

        public string? SourceUrl { get; set; }

        public Category? Category { get; set; }

        public Country? Country { get; set; }

    }
}
