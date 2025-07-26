using Backend.Application.Features.TalesManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleResponse
    {
        public TaleDraftSummary? Tale { get; set; }

    }
}
