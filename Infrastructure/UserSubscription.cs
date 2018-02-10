namespace Infrastructure
{
    public class UserSubscription
    {
        public UserSubscription(string userId, string subscription)
        {
            UserId = userId;
            Subscription = subscription;
        }

        public string UserId { get; }

        public string Subscription { get; }
    }
}