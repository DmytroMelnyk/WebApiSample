using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure
{
    public class UserSubscription
    {
        public UserSubscription(string userId, string subscription)
        {
            Id = new UserSubscriptionId(userId, subscription);
        }

        [BsonId]
        public UserSubscriptionId Id { get; set; }

        public class UserSubscriptionId
        {
            public UserSubscriptionId(string userId, string subscription)
            {
                UserId = userId;
                Subscription = subscription;
            }

            public string UserId { get; }

            public string Subscription { get; }
        }
    }
}