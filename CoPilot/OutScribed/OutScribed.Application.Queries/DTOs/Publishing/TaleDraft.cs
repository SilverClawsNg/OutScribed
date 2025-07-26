using OutScribed.Application.Queries.DTOs.Tagging;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.DTOs.Publishing
{
    public class TaleDraft
    {
        public int Id { get; set; }

        public Ulid TaleId { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public string Title { get; set; } = default!;

        public string? Summary { get; set; }

        public string? Text { get; set; }

        public string? Photo { get; set; }

        public TaleStatus Status { get; set; }

        public Country? Country { get; set; }

        public Category Category { get; set; }

        public RatingType MyRating { get; set; } = default!;

        public int ViewsCount { get; set; }

        public int RatesCount { get; set; }

        public int FollowersCount { get; set; }

        public int CommentsCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }

        public int SharesCount { get; set; }

        public int FlagsCount { get; set; }

        public ICollection<Tag> Tags { get; set; } = [];

        public ICollection<TaleHistory> Histories { get; set; } = [];

    }
}
