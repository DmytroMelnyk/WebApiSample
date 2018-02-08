using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IFeedItemSource : IObservable<FeedItem>
    {
    }

    public interface IFeedItemSink : IObserver<FeedItem>
    {
    }

    public interface IFeedItemRepository
    {
        Task<FeedItem> GetAsync(string id);
    }

    

    interface IFeedSourceRepository
    {
        
    }

    interface IUserSettingsRepository
    {
        Task AddUserAsync(User user);

        Task AddSubscriptionAsync(string id, string subscription);

        Task<IEnumerable<string>> GetSubscriptionsAsync(string id);
    }

    internal class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public IEnumerable<string> Subscriptions { get; set; }
    }

    class Manager
    {
        public Manager(IFeedItemSource feedSource, IFeedItemSink feedSink)
        {
            feedSource.Subscribe(feedSink);
        }
    }
}
