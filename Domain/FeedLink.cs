using System;

namespace Domain
{
    public class FeedLink
    {
        public FeedLink(Uri uri, string title, DateTimeOffset lastUpdated)
        {
            Uri = uri;
            Title = title;
            LastUpdated = lastUpdated;
        }

        public Uri Uri { get; }
        public string Title { get; }
        public DateTimeOffset LastUpdated { get; }
    }
}