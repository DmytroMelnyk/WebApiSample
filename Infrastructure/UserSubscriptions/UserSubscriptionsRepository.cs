using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.UserSubscriptions;
using Infrastructure.Extensions;
using MongoDB.Driver;

namespace Infrastructure.UserSubscriptions
{
    public class UserSubscriptionsRepository : IUserSubscriptionsRepository, IConfigurableRepository
    {
        private static readonly Collation UserSubscriptionCollation =
            new Collation("en", strength: new Optional<CollationStrength?>(CollationStrength.Primary));

        private readonly IMongoDatabase _mongoDatabase;
        
        public UserSubscriptionsRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<UserSubscription> Users => _mongoDatabase.GetCollection<UserSubscription>("users");

        public Task AddSubscriptionAsync(string userId, string subscription) => Users
            .InsertOneAsync(new UserSubscription(userId, subscription));

        public Task RemoveSubscriptionAsync(string userId, string subscription) => Users
            .DeleteOneAsync(x => x.Subscription == subscription && x.UserId == userId, new DeleteOptions
            {
                Collation = UserSubscriptionCollation
            });

        public IObservable<string> GetSubscriptionsAsync(string userId) => Users
            .FindAll(x => x.UserId == userId, new FindOptions<UserSubscription> 
            {
                Collation = UserSubscriptionCollation
            })
            .Select(x => x.Subscription);

        void IConfigurableRepository.Configure()
        {
            Users.Indexes.CreateOne(
                Builders<UserSubscription>.IndexKeys.Ascending(x => x.UserId).Ascending(x => x.Subscription),
                new CreateIndexOptions
                {
                    Unique = true,
                    Collation = UserSubscriptionCollation
                });
        }
    }
}
