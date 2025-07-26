using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Queries.LoadTaleComments
{
    public record LoadTaleCommentsQuery(Guid AccountId, Guid TaleId, string? Username, string? Keyword, SortTypes? Sort, int? Pointer, int? Size)
        : IRequest<LoadTaleCommentsQueryResponse>
    {
        public Guid AccountId { get; set; } = AccountId;

        public Guid TaleId { get; set; } = TaleId;

        public string? Username { get; set; } = Username;

        public string? Keyword { get; set; } = Keyword;

        public SortTypes? Sort { get; set; } = Sort;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
