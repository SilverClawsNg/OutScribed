using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchList
{
    public record LoadFullWatchListQuery(Guid Id, Guid AccountId)
        : IRequest<LoadFullWatchListQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid Id { get; set; } = Id;

    }
}
