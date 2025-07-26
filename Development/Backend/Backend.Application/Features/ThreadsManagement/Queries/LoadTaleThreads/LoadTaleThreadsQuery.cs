using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Queries.LoadTaleThreads
{
    public record LoadTaleThreadsQuery(Guid AccountId, Guid Id, Categories? Category, 
        SortTypes? Sort, string? Keyword, int? Pointer, int? Size)
        : IRequest<LoadTaleThreadsQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public Guid Id { get; set; } = Id;

        public Categories? Category { get; set; } = Category;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
