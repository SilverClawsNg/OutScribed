using Backend.Application.Features.UserManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.UserManagement.Queries.LoadActivities
{

    public class LoadActivitiesQueryResponse
    {

        public ActivityTypes? Type { get; set; }

        public bool? HasRead { get; set; }

        public string? Keyword { get; set; }

        public int Pointer { get; set; }

        public int Size { get; set; }

        public int Counter { get; set; }

        public bool Previous { get; set; }

        public bool Next { get; set; }

        public SortTypes? Sort { get; set; }

        public List<ActivitySummary>? Activities { get; set; }

    }

}