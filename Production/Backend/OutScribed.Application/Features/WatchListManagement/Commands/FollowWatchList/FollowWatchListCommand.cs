using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.WatchListManagement.Commands.FollowWatchList
{
    public class FollowWatchListCommand : IRequest<Result<FollowWatchListResponse>>
    {
        public Guid WatchListId { get; set; }

        public Guid AccountId { get; set; }

        public bool Option { get; set; }

    }
}
