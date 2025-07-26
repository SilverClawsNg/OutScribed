using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Commenting.Application.Features.Queries.LoadAllComments
{
    public class LoadAllCommentsRequest
    {
        public Guid ContentId { get; set; }

        public SortType? Sort { get; set; }

        public string? Keyword { get; set; }

        public int? Pointer { get; set; }

        public int? Size { get; set; }
    }
}
