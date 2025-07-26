using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TempUserManagement.Commands.VerifyOTPPhone
{
    public class VerifyOTPPhoneCommand : IRequest<Result<bool>>
    {
        public string PhoneNumber { get; set; } = null!;

        public int Otp { get; set; }
    }
}
