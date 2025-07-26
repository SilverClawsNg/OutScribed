using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleLinks
{
    public record LoadTaleLinksQuery(Guid UserId, SortTypes? Sort, string? Keyword, 
        int? Pointer, int? Size)
        : IRequest<LoadTaleLinksQueryResponse>
    {
        public Guid UserId { get; set; } = UserId;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
