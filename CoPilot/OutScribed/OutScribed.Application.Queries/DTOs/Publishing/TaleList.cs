using OutScribed.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Application.Queries.DTOs.Publishing
{
    public class TaleList
    {
        public int Id { get; set; }

        public Ulid TaleId { get; set; }

        public DateTime CreatedAt { get; set; } = default!;

        public Ulid AccountId { get; set; }

        public string Title { get; set; } = default!;

        public string Summary { get; set; } = default!;

        public string Text { get; set; } = default!;

        public string Photo { get; set; } = default!;

        public Country? Country { get; set; }

        public Category Category { get; set; }

        public bool IsFollowing { get; set; }

        public RatingType MyRating { get; set; } = default!;

        public int ViewsCount { get; set; }

        public int RatesCount { get; set; }

        public int FollowersCount { get; set; }

        public int CommentsCount { get; set; }

        public int UpvotesCount { get; set; }

        public int DownvotesCount { get; set; }

        public int SharesCount { get; set; }

        public int FlagsCount { get; set; }
    }
}
