using MediatR;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUsernameProfile
{
    public record LoadUsernameProfileQuery(string Id, Guid AccountId)
        : IRequest<LoadUsernameProfileQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public string Id { get; set; } = Id;

    }
}
