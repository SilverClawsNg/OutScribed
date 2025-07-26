using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTale
{
    public class CreateTaleResponse
    {
        public TaleDraftSummary? Tale { get; set; }

    }
}
