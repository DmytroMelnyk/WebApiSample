using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IFeedItemRepository
    {
        Task AddAsync(FeedItem item);

        Task<FeedItem> GetAsync(string id);
    }

    public interface IFeedItemSource
    {
        IObservable<FeedItem> Items { get; }
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
        private readonly IFeedItemSource _feedSource;
        private readonly IFeedItemRepository _feedRepository;

        public Manager(IFeedItemSource feedSource, IFeedItemRepository feedRepository)
        {
            _feedSource = feedSource;
            _feedRepository = feedRepository;
        }
    }
}
