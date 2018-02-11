using System;
using System.Reactive.Linq;
using Domain.FeedItems;
using Domain.UserSubscriptions;

namespace Domain
{
    public class FeedItemsProvider
    {
        private readonly IFeedItemRepository _feedItemRepository;
        private readonly IUserSubscriptionsRepository _subscriptionsRepository;

        public FeedItemsProvider(IFeedItemRepository feedItemRepository, IUserSubscriptionsRepository subscriptionsRepository)
        {
            _feedItemRepository = feedItemRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public IObservable<FeedItem> GetFeedItems(string userId) => _subscriptionsRepository
            .GetSubscriptionsAsync(userId)
            .SelectMany(_feedItemRepository.GetItems);
    }
}
