using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.WatchListManagement.Queries.LoadFullWatchLists
{
    public record LoadFullWatchListsQuery(Guid UserId, Categories? Category, Countries? Country,
        SortTypes? Sort, string? Keyword, int? Pointer, int? Size)
        : IRequest<LoadFullWatchListsQueryResponse>
    {
        public Guid UserId { get; set; } = UserId;

        public Categories? Category { get; set; } = Category;

        public Countries? Country { get; set; } = Country;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
