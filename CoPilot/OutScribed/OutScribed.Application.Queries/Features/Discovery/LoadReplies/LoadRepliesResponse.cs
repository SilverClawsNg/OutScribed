using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.Application.Queries.DTOs.Discovery;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Discovery.LoadReplies
{
    public class LoadRepliesResponse
    {
        public Ulid WatchlistId { get; set; }

        public Ulid CommentId { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<WatchlistComment>? Replies { get; set; }

    }
}
