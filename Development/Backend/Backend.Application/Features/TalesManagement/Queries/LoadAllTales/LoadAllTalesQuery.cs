using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadAllTales
{
    public record LoadAllTalesQuery(Guid AccountId, Categories? Category,
        Countries? Country, string? Username, SortTypes? Sort, Guid? WatchlistId, string? Tag, string? Keyword, int? Pointer, int? Size)
        : IRequest<LoadAllTalesQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public Countries? Country { get; set; } = Country;

        public Categories? Category { get; set; } = Category;

        public string? Username { get; set; } = Username;

        public Guid? WatchlistId { get; set; } = WatchlistId;

        public string? Keyword { get; set; } = Keyword;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Tag { get; set; } = Tag;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
