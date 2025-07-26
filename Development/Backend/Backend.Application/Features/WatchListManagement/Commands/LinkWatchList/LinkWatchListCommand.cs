using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.WatchListManagement.Commands.LinkWatchList
{
    public class LinkWatchListCommand : IRequest<Result<LinkWatchListResponse>>
    {
        public Guid WatchListId { get; set; }

        public Guid TaleId { get; set; }

    }
}
