using OutScribed.Domain.Enums;
using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.ThreadsManagement.Commands.PublishThread
{
    public class PublishThreadCommand : IRequest<Result<bool>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

    }
}
