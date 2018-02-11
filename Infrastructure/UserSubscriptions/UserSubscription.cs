using MongoDB.Bson;

namespace Infrastructure.UserSubscriptions
{
    public class UserSubscription
    {
        public UserSubscription(string userId, string subscription)
        {
            Id = ObjectId.GenerateNewId();
            UserId = userId;
            Subscription = subscription;
        }

        public ObjectId Id { get; private set; }

        public string UserId { get; private set; }

        public string Subscription { get; private set; }
    }
}