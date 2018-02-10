using System;
using System.Threading.Tasks;
using Domain.Users;
using MongoDB.Driver;

namespace Infrastructure
{
    public class UserSettingsRepository : IUserSettingsRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public UserSettingsRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public IMongoCollection<User> Users => _mongoDatabase.GetCollection<User>("users");

        public Task AddUserAsync(User user) => Users.InsertOneAsync(user);

        public Task AddSubscriptionAsync(string id, string subscription)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(string id)
        {
            var cursor = await Users.FindAsync(x => x.Id == id);
            return await cursor.FirstAsync();
        }
    }
}
