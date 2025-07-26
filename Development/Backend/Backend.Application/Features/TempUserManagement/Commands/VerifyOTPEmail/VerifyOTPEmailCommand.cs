using Backend.Domain.Exceptions;
using MediatR;

namespace Backend.Application.Features.TempUserManagement.Commands.VerifyOTPEmail
{
    public class VerifyOTPEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;

        public int Otp { get; set; }
    }
}
