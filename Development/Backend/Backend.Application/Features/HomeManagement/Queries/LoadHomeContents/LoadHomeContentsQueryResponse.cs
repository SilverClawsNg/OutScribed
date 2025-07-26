using Backend.Application.Features.TalesManagement.Common;
using Backend.Application.Features.ThreadsManagement.Common;
using Backend.Application.Features.UserManagement.Common;
using Backend.Application.Features.WatchListManagement.Common;
using Backend.Domain.Enums;

namespace Backend.Application.Features.HomeManagement.Queries.LoadHomeContents
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