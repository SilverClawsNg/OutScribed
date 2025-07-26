using OutScribed.Application.Queries.DTOs.Tagging;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Analysis
{
    public class InsightDraft
    {
        public int Id { get; set; }

        public Ulid InsightId { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public string TaleSlug { get; set; } = default!;

        public string TaleTitle { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string Details { get; set; } = default!;

        public string PhotoUrl { get; set; } = default!;

        public bool IsOnline { get; set; }

        public int ViewsCount { get; set; }

        public int RatesCount { get; set; }

        public int FollowersCount { get; set; }

        public int CommentsCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }

        public int SharesCount { get; set; }

        public int FlagsCount { get; set; }

        public Country? Country { get; set; }

        public Category? Category { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];

        public ICollection<InsightAddendum> Addendums { get; set; } = [];

    }
}
