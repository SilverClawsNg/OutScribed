using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TempUserManagement.Commands.VerifyOTPPhone
{
    public class VerifyOTPPhoneCommand : IRequest<Result<bool>>
    {
        public string PhoneNumber { get; set; } = null!;

        public int Otp { get; set; }
    }
}
