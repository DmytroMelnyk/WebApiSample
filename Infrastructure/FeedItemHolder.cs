using Domain.FeedItems;
using MongoDB.Bson;

namespace Infrastructure
{
    public class FeedItemHolder
    {
        public FeedItemHolder(ObjectId id, string source, FeedItem feedItem)
        {
            Id = id;
            Source = source;
            FeedItem = feedItem;
        }

        public ObjectId Id { get; }

        public string Source { get; }

        public FeedItem FeedItem { get; }
    }
}