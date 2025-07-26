using OutScribed.Domain.Enums;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadComments
{
    public record LoadThreadCommentsQuery(Guid AccountId, Guid ThreadId, string? Username, string? Keyword,
        SortTypes? Sort, int? Pointer, int? Size)
        : IRequest<LoadThreadCommentsQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid ThreadId { get; set; } = ThreadId;

        public string? Username { get; set; } = Username;

        public string? Keyword { get; set; } = Keyword;

        public SortTypes? Sort { get; set; } = Sort;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
