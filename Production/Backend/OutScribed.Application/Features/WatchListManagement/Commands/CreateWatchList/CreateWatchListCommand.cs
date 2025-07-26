using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.WatchListManagement.Commands.CreateWatchList
{
    public class CreateWatchListCommand : IRequest<Result<CreateWatchListResponse?>>
    {

        public Guid AdminId { get; set; }

        public string Title { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string SourceUrl { get; set; } = null!;

        public string SourceText { get; set; } = null!;

        public Categories? Category { get; set; }

        public Countries? Country { get; set; }

    }
}
