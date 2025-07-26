using OutScribed.Application.Features.TalesManagement.Common;
using OutScribed.Application.Features.ThreadsManagement.Common;
using OutScribed.Application.Features.UserManagement.Common;
using OutScribed.Application.Features.WatchListManagement.Common;
using OutScribed.Domain.Enums;

namespace OutScribed.Application.Features.HomeManagement.Queries.LoadHomeContents
{

    public class LoadHomeContentsQueryResponse
    {

        public List<ThreadsSummary>? PopularThreads { get; set; }

        public List<TaleSummary>? PopularTales { get; set; }

        public List<ThreadsSummary>? RecentThreads { get; set; }

        public List<TaleSummary>? RecentTales { get; set; }

        public List<WatchListSummary>? RecentWatchLists { get; set; }

        public List<WriterSummary>? PopularWriters { get; set; }

        public List<WriterSummary>? RecentWriters { get; set; }

    }


}