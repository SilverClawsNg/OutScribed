using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTalePhoto
{
    public class UpdateTalePhotoCommand : IRequest<Result<UpdateTalePhotoResponse>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Base64String { get; set; } = null!;

    }
}
