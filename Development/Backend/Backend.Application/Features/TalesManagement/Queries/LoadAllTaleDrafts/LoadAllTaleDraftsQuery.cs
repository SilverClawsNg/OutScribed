using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadAllTaleDrafts
{
    public record LoadAllTaleDraftsQuery(TaleStatuses? Status, Categories? Category,
        Countries? Country, string? Username, SortTypes? Sort, string? Keyword, int? Pointer, int? Size)
        : IRequest<LoadAllTaleDraftsQueryResponse>
    {

        public TaleStatuses? Status { get; set; } = Status;

        public Categories? Category { get; set; } = Category;

        public Countries? Country { get; set; } = Country;

        public string? Username { get; set; } = Username;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
