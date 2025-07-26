using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Publishing.Application.Features.Queries.LoadAdminDrafts
{
    public class LoadAdminDraftsRequest
    {
        public TaleStatus? Status { get; set; }

        public Country? Country { get; set; }

        public Category? Category { get; set; }

        public string? Username { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
