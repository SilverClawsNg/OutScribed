using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Application.Features.Queries.LoadWriters
{
    public record LoadWritersRequest
    {
        public Country? Country { get; set; }

        public string? Username { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
