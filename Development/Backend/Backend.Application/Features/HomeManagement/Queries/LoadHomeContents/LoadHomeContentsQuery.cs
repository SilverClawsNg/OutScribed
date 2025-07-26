using MediatR;

namespace Backend.Application.Features.HomeManagement.Queries.LoadHomeContents
{
    public record LoadHomeContentsQuery(Guid AccountId)
        : IRequest<LoadHomeContentsQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

    }
}
