namespace Infrastructure.UserSubscriptions
{
    public class UserSubscription
    {
        public UserSubscription(string userId, string subscription)
        {
            UserId = userId;
            Subscription = subscription;
        }

        public string Id { get; private set; }

        public string UserId { get; private set; }

        public string Subscription { get; private set; }
    }
}