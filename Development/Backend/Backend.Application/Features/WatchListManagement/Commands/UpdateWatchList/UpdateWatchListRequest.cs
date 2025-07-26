using Backend.Domain.Enums;

namespace Backend.Application.Features.WatchListManagement.Commands.UpdateWatchList
{
    public class UpdateWatchListRequest
    {

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string SourceUrl { get; set; } = null!;

        public string SourceText { get; set; } = null!;

        public Categories? Category { get; set; }

        public Countries? Country { get; set; }
    }
}
