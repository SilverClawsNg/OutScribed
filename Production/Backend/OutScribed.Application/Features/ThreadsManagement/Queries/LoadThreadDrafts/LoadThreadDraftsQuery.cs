using OutScribed.Domain.Enums;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Queries.LoadThreadDrafts
{
    public record LoadThreadDraftsQuery(Guid AccountId, Categories? Category,
       Countries? Country, bool? IsOnline, SortTypes? Sort, string? Keyword, int? Pointer, int? Size)
        : IRequest<LoadThreadDraftsQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public Countries? Country { get; set; } = Country;

        public Categories? Category { get; set; } = Category;

        public bool? IsOnline { get; set; } = IsOnline;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
