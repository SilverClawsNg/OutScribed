using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadShares
{
    public class LoadSharesRequest
    {
        public Ulid TaleId { get; set; }

        public ContactType? Type { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
