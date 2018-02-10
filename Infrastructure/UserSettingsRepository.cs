using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.Users;
using Infrastructure.Extensions;
using MongoDB.Driver;

namespace Infrastructure
{
    public class UserSubscriptionsRepository : IUserSubscriptionsRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public UserSubscriptionsRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<UserSubscription> Users => _mongoDatabase.GetCollection<UserSubscription>("users");

        public Task AddSubscriptionAsync(string userId, string subscription) => Users
            .InsertOneAsync(new UserSubscription(userId, subscription));

        public Task RemoveSubscriptionAsync(string userId, string subscription) => Users
            .DeleteOneAsync(x => x.Id.Subscription == subscription && x.Id.UserId == userId);

        public IObservable<string> GetSubscriptionsAsync(string userId) => Users
            .FindAll(x => x.Id.UserId == userId)
            .Select(x => x.Id.Subscription);
    }
}
