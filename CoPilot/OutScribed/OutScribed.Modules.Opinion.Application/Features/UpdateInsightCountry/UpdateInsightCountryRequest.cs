using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Analysis.Application.Features.UpdateInsightCountry
{
    public class UpdateInsightCountryRequest
    {
        public Guid? Id { get; set; }

        public Country? Country { get; set; }
    }
}
