using OutScribed.Application.Queries.DTOs.Identity;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Application.Queries.Features.Identity.LoadWriters
{
    public class LoadWritersResponse
    {
        public Country? Country { get; set; }

        public string? Username { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }

        public bool HasNext { get; set; }

        public List<AccountWriter>? Writers { get; set; }

    }
}
