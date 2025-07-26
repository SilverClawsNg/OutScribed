using OutScribed.Application.Queries.DTOs.Publishing;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadShares
{
    public class LoadSharesResponse
    {
        public Ulid TaleId { get; set; }

        public SortType? Sort { get; set; }

        public ContactType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<TaleShare>? Shares { get; set; }

    }
}
