using OutScribed.Domain.Enums;
using MediatR;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadActivities
{
    public record LoadActivitiesQuery(Guid AccountId, ActivityTypes? Type, 
       bool? HasRead, string? Keyword, SortTypes? Sort, int? Pointer, int? Size)
        : IRequest<LoadActivitiesQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public ActivityTypes? Type { get; set; } = Type;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Keyword { get; set; } = Keyword;

        public bool? HasRead { get; set; } = HasRead;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
