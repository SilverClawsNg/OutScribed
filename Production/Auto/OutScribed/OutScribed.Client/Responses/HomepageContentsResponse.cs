using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class HomepageContentsResponse
    {
        public List<ThreadsSummary>? PopularThreads { get; set; }

        public List<TaleSummary>? PopularTales { get; set; }

        public List<ThreadsSummary>? RecentThreads { get; set; }

        public ThreadsSummary? FeaturedThread => RecentThreads?.FirstOrDefault();

        public List<ThreadsSummary>? OtherThreads => RecentThreads == null ? null : [.. RecentThreads.Skip(1)];

        public List<TaleSummary>? RecentTales { get; set; }

        public TaleSummary? FeaturedTale => RecentTales?.FirstOrDefault();

        public List<TaleSummary>? OtherTales => RecentTales == null ? null : [.. RecentTales.Skip(1)];

        public List<WatchListSummary>? RecentWatchLists { get; set; }

        public List<WriterSummary>? PopularWriters { get; set; }

        public List<WriterSummary>? RecentWriters { get; set; }
    }
}
