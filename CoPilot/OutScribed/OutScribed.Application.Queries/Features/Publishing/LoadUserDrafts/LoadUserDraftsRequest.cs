using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadUserDrafts
{
    public class LoadUserDraftsRequest
    {
        public TaleStatus? Status { get; set; }

        public Country? Country { get; set; }

        public Category? Category { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
