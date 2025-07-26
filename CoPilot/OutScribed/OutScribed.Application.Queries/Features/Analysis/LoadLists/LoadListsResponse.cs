using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Analysis.LoadLists
{

    public class LoadListsResponse
    {

        public Ulid? TaleId { get; set; }

        public Country? Country { get; set; }

        public Category? Category { get; set; }

        public string? Username { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<InsightList>? Insights { get; set; }

    }

}
