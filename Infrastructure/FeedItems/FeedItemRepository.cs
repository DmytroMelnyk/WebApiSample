using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.FeedItems;
using Infrastructure.Extensions;
using MongoDB.Driver;

namespace Infrastructure.FeedItems
{
    public class FeedItemRepository : IFeedItemSink, IFeedItemRepository
    {
        private static readonly Collation FeedItemCollations =
            new Collation("en", strength: new Optional<CollationStrength?>(CollationStrength.Primary));

        private readonly IMongoDatabase _database;

        public FeedItemRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<FeedItemHolder> FeedItems => _database.GetCollection<FeedItemHolder>("feedItems");

        public Task OnNextFeedItemAsync(FeedItem item, string sourceName) => FeedItems
            .UpdateOneAsync(
                x => x.FeedItem.Id == item.Id,
                Builders<FeedItemHolder>.Update.Set(x => x.FeedItem, item).Set(x => x.Source, sourceName),
                new UpdateOptions
                {
                    IsUpsert = true,
                    Collation = FeedItemCollations
                });

        public IObservable<FeedItem> GetItems(string source) => FeedItems
            .FindAll(x => x.Source == source, new FindOptions<FeedItemHolder>
            {
                Collation = FeedItemCollations
            })
            .Select(x => x.FeedItem);
    }
}