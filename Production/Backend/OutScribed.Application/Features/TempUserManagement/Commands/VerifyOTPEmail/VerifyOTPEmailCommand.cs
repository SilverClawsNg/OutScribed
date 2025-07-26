using OutScribed.Domain.Exceptions;
using MediatR;

namespace OutScribed.Application.Features.TempUserManagement.Commands.VerifyOTPEmail
{
    public class VerifyOTPEmailCommand : IRequest<Result<bool>>
    {
        public string EmailAddress { get; set; } = null!;

        public int Otp { get; set; }
    }
}
