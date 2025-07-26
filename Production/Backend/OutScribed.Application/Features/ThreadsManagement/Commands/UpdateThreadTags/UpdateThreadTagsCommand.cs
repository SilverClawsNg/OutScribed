using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.UpdateThreadTags
{
    public class UpdateThreadTagsCommand : IRequest<Result<UpdateThreadTagsResponse>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Tags { get; set; } = null!;

    }
}
