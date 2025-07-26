using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TalesManagement.Commands.UpdateTaleDetails
{
    public class UpdateTaleDetailsCommand : IRequest<Result<UpdateTaleDetailsResponse>>
    {

        public Guid Id { get; set; }

        public Guid AdminId { get; set; }

        public string Details { get; set; } = null!;

    }
}
