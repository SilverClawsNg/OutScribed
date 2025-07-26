using MediatR;

namespace OutScribed.Application.Features.WatchListManagement.Queries.LoadFullWatchList
{
    public record LoadFullWatchListQuery(Guid Id, Guid AccountId)
        : IRequest<LoadFullWatchListQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid Id { get; set; } = Id;

    }
}
