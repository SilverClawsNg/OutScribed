using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Onboarding.Application.Features.CreateTempUser
{
    public record CreateTempUserCommand(string Email, string IpAddress) : IRequest;
}
