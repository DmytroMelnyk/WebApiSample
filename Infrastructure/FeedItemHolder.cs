namespace Domain.FeedItems
{
    public class FeedItemHolder
    {
        public FeedItemHolder(string id, string source, FeedItem feedItem)
        {
            Id = id;
            Source = source;
            FeedItem = feedItem;
        }

        public string Id { get; }

        public string Source { get; }

        public FeedItem FeedItem { get; }
    }
}