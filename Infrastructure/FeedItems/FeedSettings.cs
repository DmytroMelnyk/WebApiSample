namespace Infrastructure.FeedItems
{
    public class FeedSettings
    {
        public string Url { get; set; }

        public string SourceName { get; set; }

        public int PollingIntervalMin { get; set; }
    }
}