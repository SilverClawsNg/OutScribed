using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TalesManagement.Commands.UpdateTaleTags
{
    public class UpdateTaleTagsCommand : IRequest<Result<UpdateTaleTagsResponse>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Tags { get; set; } = null!;

    }
}
