using MediatR;

namespace Backend.Application.Features.UserManagement.Queries.LoadUserProfile
{
    public record LoadUserProfileQuery(Guid Id, Guid AccountId)
        : IRequest<LoadUserProfileQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public Guid Id { get; set; } = Id;

    }
}
