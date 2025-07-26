using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadFollows
{
    public class LoadFollowsResponse
    {
        public Ulid TaleId { get; set; }

        public SortType? Sort { get; set; }

        public string? Username { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<TaleFollow>? Follows { get; set; }

    }
}
