using OutScribed.Modules.Discovery.Domain.Models;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Discovery.LoadComments
{
    public class LoadCommentsResponse
    {
        public Ulid WatchlistId { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<Comment>? Comments { get; set; }

    }
}
