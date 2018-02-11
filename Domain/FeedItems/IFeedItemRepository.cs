using System;

namespace Domain.FeedItems
{
    public interface IFeedItemRepository
    {
        IObservable<FeedItem> GetItems(string source);
    }
}
