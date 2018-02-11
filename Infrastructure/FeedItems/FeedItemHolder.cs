using Domain.FeedItems;
using MongoDB.Bson;

namespace Infrastructure.FeedItems
{
    public class FeedItemHolder
    {
        public ObjectId Id { get; private set; }

        public string Source { get; private set; }

        public FeedItem FeedItem { get; private set; }
    }
}