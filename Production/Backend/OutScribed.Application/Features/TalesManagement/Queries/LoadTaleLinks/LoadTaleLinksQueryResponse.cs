using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.TalesManagement.Queries.LoadTaleLinks
{

    public class LoadTaleLinksQueryResponse
    {
      
        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool More { get; set; }

        public SortTypes? Sort { get; set; }

        public string? Keyword { get; set; }

        public List<TaleLink>? Tales { get; set; }

    }

}