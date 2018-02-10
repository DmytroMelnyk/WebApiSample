using System;
using System.Collections.Generic;
using System.Linq;
using Domain.FeedItems;

namespace Domain
{
    public class FeedItemSourceSubscriptionManager : IDisposable
    {
        private readonly IEnumerable<IDisposable> _subscriptions;

        public FeedItemSourceSubscriptionManager(FeedItemSourceProvider provider, IFeedItemSink feedSink)
        {
            _subscriptions = provider.GetAll().Select(x => x.Subscribe(feedSink)).ToList();
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }
    }
}