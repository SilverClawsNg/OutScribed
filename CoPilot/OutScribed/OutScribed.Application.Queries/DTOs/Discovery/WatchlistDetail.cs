using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.Application.Queries.DTOs.Tagging;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Discovery
{
    public class WatchlistDetail
    {

        public int Id { get; set; }

        public Ulid WatchlistId { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public string Summary { get; set; } = default!;

        public string SourceText { get; set; } = default!;

        public string SourceUrl { get; set; } = default!;

        public Country? Country { get; set; }

        public Category Category { get; set; }

        public bool IsFollowing { get; set; }

        public RatingType MyRating { get; set; }

        public int TalesCount { get; set; }

        public int ViewsCount { get; set; }

        public int RatesCount { get; set; }

        public int FollowersCount { get; set; }

        public int CommentsCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }

        public int SharesCount { get; set; }

        public int FlagsCount { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];

        public ICollection<WatchlistComment> Comments { get; set; } = [];

    }
}
