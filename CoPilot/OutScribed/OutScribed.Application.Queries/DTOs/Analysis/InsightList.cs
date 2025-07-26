using OutScribed.Application.Queries.DTOs.Tagging;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Analysis
{

    public class InsightList
    {

        public int Id { get; set; }

        public Ulid InsightId { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public string Title { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string Photo { get; set; } = default!;

        public string Slug { get; set; } = default!;

        public Ulid TaleId { get; set; }

        public Country? Country { get; set; }

        public Category? Category { get; set; }

        public bool IsFollowing { get; set; }

        public int ViewsCount { get; set; }

        public int FollowersCount { get; set; }

        public int CommentsCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }

        public int SharesCount { get; set; }

        public int FlagsCount { get; set; }

        public int PopularityScore { get; set; }

        public ICollection<TagSummary> Tags { get; set; } = [];

    }

}
