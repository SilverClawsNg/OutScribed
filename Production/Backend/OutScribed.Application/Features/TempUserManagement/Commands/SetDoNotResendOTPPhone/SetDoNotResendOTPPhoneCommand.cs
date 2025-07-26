using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TempUserManagement.Commands.SetDoNotResendOTPPhone
{
    public class SetDoNotResendOTPPhoneCommand : IRequest<Result<bool>>
    {
        public string PhoneNumber { get; set; } = null!;

    }
}
