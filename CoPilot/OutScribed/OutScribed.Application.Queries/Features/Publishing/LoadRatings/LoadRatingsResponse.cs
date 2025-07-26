using OutScribed.Application.Queries.DTOs.Publishing;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Publishing.LoadRatings
{
    public class LoadRatingsResponse
    {
        public Ulid TaleId { get; set; }

        public SortType? Sort { get; set; }

        public RatingType? Type { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<TaleRating>? Ratings { get; set; }

    }
}
