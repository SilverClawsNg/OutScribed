using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTale
{
    public record LoadTaleQuery(string Url, Guid AccountId)
        : IRequest<LoadTaleQueryResponse>
    {

        public string Url { get; set; } = Url;

        public Guid AccountId { get; set; } = AccountId;

    }
}
