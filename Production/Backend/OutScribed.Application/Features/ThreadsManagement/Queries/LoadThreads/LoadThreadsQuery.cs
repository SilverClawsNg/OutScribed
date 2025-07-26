using OutScribed.Domain.Enums;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreads
{
    public record LoadThreadsQuery(Guid AccountId, Guid? ThreaderId, Categories? Category,
       Countries? Country, string? Username, SortTypes? Sort, string? Tag, string? Keyword, int? Pointer, int? Size)
        : IRequest<LoadThreadsQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public Guid? ThreaderId { get; set; } = ThreaderId;

        public Categories? Category { get; set; } = Category;

        public Countries? Country { get; set; } = Country;

        public string? Username { get; set; } = Username;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public string? Tag { get; set; } = Tag;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
