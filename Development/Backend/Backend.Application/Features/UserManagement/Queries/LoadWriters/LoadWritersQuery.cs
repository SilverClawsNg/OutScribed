using Backend.Domain.Enums;
using MediatR;

namespace Backend.Application.Features.UserManagement.Queries.LoadWriters
{
    public record LoadWritersQuery(Guid AccountId, Countries? Country, 
        SortTypes? Sort, string? Username, int? Pointer, int? Size)
        : IRequest<LoadWritersQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public Countries? Country { get; set; } = Country;

        public SortTypes? Sort { get; set; } = Sort;

        public string? Username { get; set; } = Username;

        public int? Pointer { get; set; } = Pointer;

        public int? Size { get; set; } = Size;

    }
}
