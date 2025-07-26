using MediatR;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUser
{
    public record LoadUserQuery(string Id, Guid AccountId)
        : IRequest<LoadUserQueryResponse>
    {

        public Guid AccountId { get; set; } = AccountId;

        public string Id { get; set; } = Id;

    }
}
