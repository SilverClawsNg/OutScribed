using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadDrafts
{
    public class LoadDraftsResponse
    {
        public bool? IsOnline { get; set; }

        public Country? Country { get; set; }

        public Category? Category { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<InsightDraft>? Insights { get; set; }

    }
}
