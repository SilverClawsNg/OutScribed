using OutScribed.Domain.Enums;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTaleResponses
{
    public record LoadTaleResponsesQuery(Guid AccountId, Guid ParentId, string? Username, string? Keyword,
        SortTypes? Sort, int? Pointer, int? Size)
        : IRequest<LoadTaleResponsesQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid ParentId { get; set; } = ParentId;

        public string? Username { get; set; } = Username;

        public string? Keyword { get; set; } = Keyword;

        public SortTypes? Sort { get; set; } = Sort;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
