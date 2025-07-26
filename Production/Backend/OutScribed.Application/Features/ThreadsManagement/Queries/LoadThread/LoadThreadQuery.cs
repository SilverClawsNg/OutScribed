using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThread
{
    public record LoadThreadQuery(string Url, Guid AccountId)
        : IRequest<LoadThreadQueryResponse>
    {

        public string Url { get; set; } = Url;

        public Guid AccountId { get; set; } = AccountId;

    }
}
