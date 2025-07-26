using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.WatchListManagement.Commands.CreateWatchList
{
    public class CreateWatchListRequest
    {

        public string Title { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string SourceUrl { get; set; } = null!;

        public string SourceText { get; set; } = null!;

        public Categories? Category { get; set; }

        public Countries? Country { get; set; }

    }
}
