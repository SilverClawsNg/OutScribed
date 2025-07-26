using OutScribed.Application.Queries.DTOs.Publishing;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadDropdowns
{
    public class LoadDropdownsResponse
    {
        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public TaleBasic? Tales { get; set; }

    }
}
