using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadFlags
{
    public class LoadFlagsRequest
    {
        public Ulid TaleId { get; set; }

        public SortType? Sort { get; set; }

        public FlagType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
