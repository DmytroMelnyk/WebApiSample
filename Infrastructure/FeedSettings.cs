using System;

namespace Infrastructure
{
    public class FeedSettings
    {
        public FeedSettings(string url, string sourceName, TimeSpan pollingInterval)
        {
            Url = url;
            SourceName = sourceName;
            PollingInterval = pollingInterval;
        }

        public string Url { get; }

        public string SourceName { get; }

        public TimeSpan PollingInterval { get; }
    }
}