using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.FeedItems;
using MongoDB.Driver;

namespace Infrastructure
{
    public class FeedItemRepository : IFeedItemSink, IFeedItemRepository
    {
        private readonly IMongoDatabase _database;

        public FeedItemRepository(IMongoDatabase database)
        {
            _database = database;
        }

        private IMongoCollection<FeedItemHolder> FeedItems => _database.GetCollection<FeedItemHolder>("feedItems");

        public Task OnNextFeedItemAsync(FeedItemHolder item) => FeedItems
            .UpdateOneAsync(
                x => x.FeedItem.Id == item.FeedItem.Id,
                Builders<FeedItemHolder>.Update.Set(x => x.FeedItem, item.FeedItem),
                new UpdateOptions
                {
                    IsUpsert = true
                });

        public async Task<FeedItemHolder> GetAsync(string id)
        {
            var cursor = await FeedItems.FindAsync(x => x.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public IObservable<FeedItemHolder> GetItems(string source) => Observable
            .FromAsync(ct => FeedItems.FindAsync(x => x.Source == source, null, ct))
            .SelectMany(cursor => Observable
                .FromAsync(cursor.MoveNextAsync)
                .Repeat()
                .TakeWhile(x => x)
                .SelectMany(_ => cursor.Current));
    }
}