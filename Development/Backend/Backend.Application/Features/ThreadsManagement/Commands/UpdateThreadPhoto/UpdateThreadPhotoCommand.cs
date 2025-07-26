using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.ThreadsManagement.Commands.UpdateThreadPhoto
{
    public class UpdateThreadPhotoCommand : IRequest<Result<UpdateThreadPhotoResponse>>
    {

        public Guid Id { get; set; }

        public Guid AccountId { get; set; }

        public string Base64String { get; set; } = null!;

    }
}
