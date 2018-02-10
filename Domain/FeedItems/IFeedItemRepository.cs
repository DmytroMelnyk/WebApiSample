using System;
using System.Threading.Tasks;

namespace Domain.FeedItems
{
    public interface IFeedItemRepository
    {
        Task<FeedItem> GetAsync(string id);

        IObservable<FeedItem> GetItems(string source);
    }
}
